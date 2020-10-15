using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using BlazorWithSecutiry.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithSecutiry.DAL
{
    public static class ScriptExecutionExtensions
    {
        /// <summary>Executes all T-SQL scripts located in the \Scripts folder.</summary>
        /// <param name="context">The context.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="runOnce">if set to <c>true</c> [run once].</param>
        /// <returns></returns>
        /// <exception cref="Exception">Assembly file location could not be determined!</exception>
        public static IEnumerable<string> ExecuteMigrationScripts(this DbContext context, string folderName, bool runOnce)
        {
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrEmpty(assemblyLocation))
                throw new Exception("Assembly file location could not be determined!");

            var path = Path.Combine(assemblyLocation, folderName);
            if (!Directory.Exists(path)) yield break;

            var scriptsHistory = TryGetScriptsHistory(context);

            var files = Directory.EnumerateFiles(path, "*.SQL").OrderBy(f => f).ToArray();
            yield return $"Enumerating {files.Length} SQL migration scripts from folder {path}";

            var count = 0;
            var skip = 0;

            yield return $"Executing SQL scripts in {folderName}...";
            foreach (var filePath in files)
            {
                var filename = Path.GetFileName(filePath);

                var scriptHist = scriptsHistory.FirstOrDefault(s => string.Equals(s.Filename, filename, StringComparison.OrdinalIgnoreCase));
                var md5Hash = GetMd5Hash(filePath);
                // script unchanged and already executed without error? skip.
                if (runOnce && scriptHist != null && scriptHist.Md5Hash.SequenceEqual(md5Hash) && string.IsNullOrEmpty(scriptHist.Error))
                {
                    skip++;
                    continue;
                }

                count++;

                yield return $"executing script {new FileInfo(filePath).Name}...";
                var result = ExecuteScriptFile(context, filePath);
                yield return $"success: executed script {new FileInfo(filePath).Name}";
                if (!string.IsNullOrEmpty(result)) yield return result;

                if (scriptHist == null)
                {
                    scriptHist = new ScriptsHistory { DateInsert = DateTime.Now };
                    scriptsHistory.Add(scriptHist);
                }

                scriptHist.Filename = filename;
                scriptHist.Md5Hash = md5Hash;
                scriptHist.DateExecuted = DateTime.Now;
                scriptHist.Error = result;
            }

            TrySaveScriptsHistory(context, scriptsHistory);

            if (skip > 0)
                yield return $"Skipped {skip} SQL scripts";

            if (count > 0)
                yield return $"Executed {count} SQL scripts";
        }

        public static byte[] GetMd5Hash(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }

        /// <summary>
        /// Executes the script file.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="parameters">The parameters.</param>
        public static string ExecuteScriptFile(this DbContext context, string filename, params object[] parameters)
        {
            var execScript = string.Empty;
            try
            {
                var sql = File.ReadAllText(filename);
                var sqlParts = sql.Split(new[] { "\r\nGO\r\n", "\r\ngo\r\n", "\r\nGo\r\n", "\r\nGO \r\n", "\r\ngo \r\n", "\r\nGo \r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var sqlScript in sqlParts)
                {
                    execScript = sqlScript.Trim();
                    while (execScript.EndsWith("\r\nGO", StringComparison.OrdinalIgnoreCase))
                        execScript = execScript.Remove(execScript.Length - 2).Trim();
                    if (!string.IsNullOrWhiteSpace(execScript)) context.Database.ExecuteSqlCommand(execScript, parameters);
                }

                return null;
            }
            catch (Exception ex)
            {
                // TODO: Log Critical!!
                //Console.IndentLevel = 3;
                Console.WriteLine($"\t\tError executing script {filename}", $"{execScript}\r\n{ex}");
                return $"Error executing script {filename}: {ex}";
            }
        }


        private static List<ScriptsHistory> TryGetScriptsHistory(this DbContext context)
        {
            try
            {
                var list = context.Set<ScriptsHistory>().ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<ScriptsHistory>();
            }
        }

        private static bool TrySaveScriptsHistory(DbContext context, List<ScriptsHistory> scriptsHistory)
        {
            try
            {
                var inserts = scriptsHistory.Where(s => s.Id == 0).OrderBy(s => s.Id).ToArray();
                var updates = scriptsHistory.Where(s => s.Id != 0).ToArray();
                context.Set<ScriptsHistory>().AddRange(inserts);
                context.Set<ScriptsHistory>().UpdateRange(updates);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public static class ContextMigration

    {

        /// <summary>apply any pending migrations for the context to the database. 

        /// Will create the database if it does not already exist.</summary>

        public static IEnumerable<string> Migrate(DbContext context)

        {

            // Database will be created if necessary

            var pending = context.Database.GetPendingMigrations().ToList();

            yield return $"Found {pending.Count} pending database migration(s)";


            var migrationErr = TryMigrate(context);

            if (string.IsNullOrEmpty(migrationErr))

            {

                foreach (var migrated in pending) yield return $"success: {migrated}";

            }

            else

            {

                yield return $"Error executing database migration(s): {migrationErr}";

            }

        }


        private static string TryMigrate(DbContext context)

        {

            try

            {

                context.Database.Migrate();

                return null; // success: null

            }

            catch (Exception ex)

            {

                return ex.Message;

            }

        }

    }
}

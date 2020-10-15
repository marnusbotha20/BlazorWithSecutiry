using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWithSecutiry.Models
{
    public class ScriptsHistory

    {

        /// <summary>

        /// Initializes a new instance of the <see cref="ScriptsHistory"/> class.

        /// </summary>

        public ScriptsHistory()

        {

            DateInsert = DateTime.Now;

            DateExecuted = DateTime.Now;

        }


        /// <summary>Gets or sets the identifier.</summary>

        /// <value>The identifier.</value>

        public int Id { get; set; }


        /// <summary>Gets or sets the date insert.</summary>

        /// <value>The date insert.</value>

        public DateTime DateInsert { get; set; }


        /// <summary>Gets or sets the date update.</summary>

        /// <value>The date update.</value>

        public DateTime DateExecuted { get; set; }


        /// <summary>Gets or sets the script filename.</summary>

        /// <value>The filename.</value>

        public string Filename { get; set; }


        /// <summary>Gets or sets the MD5 hash of the file contents.</summary>

        /// <value>The MD5 hash.</value>

        public byte[] Md5Hash { get; set; }


        /// <summary>Gets or sets the error if any occur during execution.</summary>

        /// <value>The error.</value>

        public string Error { get; set; }

    }
}

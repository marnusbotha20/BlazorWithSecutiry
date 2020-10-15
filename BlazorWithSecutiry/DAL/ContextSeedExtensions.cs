using System;
using System.Collections.Generic;
//using Atwork.Common;
//using Atwork.DAL.Audit;
//using Atwork.DAL.Seed;
//using Atwork.DAL.Seed.WordReporting;
using BlazorWithSecutiry.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorWithSecutiry.DAL
{
    public static class ContextSeedExtensions
    {
        private static Func<DbContext> _contextInit;

        //public static IEnumerable<string> EnsureSeeded(this ApplicationDbContext context)
        //{
        //    _contextInit = () => new ApplicationDbContext(context._environmentConnection);

        //    yield return context.Seed<ObjectStateSeed>();
        //    //yield return context.Seed<QueueStateSeed>();
        //    //yield return context.Seed<ErrorResolveSeed>();

        //    //// automation
        //    //yield return context.Seed<AutomationAudienceTypeSeed>();
        //    //yield return context.Seed<AutomationWorkflowMapAw6Seed>();
        //    //yield return context.Seed<AutomationQueueStateSeed>();

        //    //// banks
        //    //yield return context.Seed<BankSeed>();

        //    //// contact
        //    //yield return context.Seed<ContactTypeSeed>();

        //    //// attachments
        //    //yield return context.Seed<AttachTypeSeed>();
        //    ////yield return context.Seed<AttachDbSeed>();

        //    //// notification
        //    //yield return context.Seed<NotificationStateSeed>();
        //    //yield return context.Seed<NotificationTypeSeed>();
        //    //yield return context.Seed<NotificationReminderTypeSeed>();

        //    //// others
        //    //yield return context.Seed<NewsTypeSeed>();
        //    //yield return context.Seed<AnnouncementTypeSeed>();
        //    //yield return context.Seed<ExternalLinkTypeSeed>();
        //    //yield return context.Seed<DefaultTitleSeed>();
        //    //yield return context.Seed<ComplianceDocSeed>();
        //    //yield return context.Seed<CustomFieldSeed>();
        //    //yield return context.Seed<WorkflowStatusSeed>();
        //    //yield return context.Seed<PaymentMethodSeed>();
        //    //yield return context.Seed<AstResultCodeSeed>();
        //    //yield return context.Seed<AtworkCompanyDetailSeed>();
        //    //yield return context.Seed<PictureStaticTypeSeed>();
        //    //yield return context.Seed<AstuteDeclarationSeed>();
        //    //yield return context.Seed<AddressTypeSeed>();
        //    //yield return context.Seed<CurrencySeed>();            
        //    //yield return context.Seed<NeedTypeSeed>();
        //    //yield return context.Seed<AstuteCoverageLookupSeed>();
        //    //yield return context.Seed<FnaPolicyTypeSeed>();
        //    //yield return context.Seed<FnaLinkedInvestmentTypeSeed>();
        //    //yield return context.Seed<NeedsAnalysisLengthSeed>();
        //    //yield return context.Seed<FnaPolicyRelationTypeSeed>();
        //    //yield return context.Seed<FnaInvestmentTypeSeed>();
        //    //yield return context.Seed<MaritalSeed>();
        //    //yield return context.Seed<AssetLiabilityTypeSeed>();
        //    //yield return context.Seed<IncomeExpenseCategorySeed>();
        //    //yield return context.Seed<IncomeExpenseSubCategorySeed>();
        //    //yield return context.Seed<FinaMetricaVersionSeed>();
        //    //yield return context.Seed<FinaMetricaRiskGroupSeed>();
        //    //yield return context.Seed<RiskCategorySeed>();
        //    //yield return context.Seed<NoteTypeSeed>();

        //    //yield return context.Seed<InvestmentGoalTypeSeed>();
        //    //yield return context.Seed<AffordabilityTemplateSeed>();
        //    //yield return context.Seed<AffordabilityTemplateSubCategorySeed>();
        //    //yield return context.Seed<BusinessAssuranceTypeSeed>();
        //    //yield return context.Seed<AstuteContentTypeSeed>();
        //    //yield return context.Seed<AstuteProductLookupSeed>();
        //    //yield return context.Seed<AstuteQualPlanLookupSeed>();
        //    //yield return context.Seed<TimeZoneItemSeed>();

        //    //// Custom field data error
        //    //yield return context.Seed<CustomFieldTypeSeed>();

        //    //// Integration 
        //    //yield return context.Seed<IntegrationDataSourceSeed>();
        //    //yield return context.Seed<IntegrationDataDestinationSeed>();
        //    //yield return context.Seed<IntegrationPartnerSeed>();
        //    //yield return context.Seed<IntegrationOperationGroupTypeSeed>();
        //    //yield return context.Seed<IntegrationOperationTypeSeed>();
        //    //yield return context.Seed<MessageTypeSeed>();

        //    //// MNA
        //    //yield return context.Seed<CoverLevelColorSeed>();
        //    //yield return context.Seed<MedicalPlanSummarySeed>();
        //    //yield return context.Seed<ChronicDiseaseSeed>();
        //    //yield return context.Seed<MedicalBenefitClassificationSeed>();
        //    //yield return context.Seed<MedicalBenefitSeed>();
        //    //yield return context.Seed<MedicalPlanChronicSeed>();
        //    //yield return context.Seed<HospitalSeed>();
        //    //yield return context.Seed<MedicalPlanHospitalSeed>();

        //    //// Localization            
        //    //yield return context.Seed<CountrySeed>();
        //    //yield return context.Seed<LocalizationFormatSeed>();
        //    //yield return context.Seed<DataTypeSeed>();
        //    //yield return context.Seed<LocalizationSeed>();

        //    ////Word Reporting
        //    //yield return context.Seed<WordReportTypeSeed>();
        //    //yield return context.Seed<WordReportTemplateSeed>();
        //    //yield return context.Seed<ReportSectionSeed>();
        //    //yield return context.Seed<ReportBookmarkOptionSeed>();

        //    //// Permissions
        //    //yield return context.Seed<AccessTypeSeed>();
        //    //yield return context.Seed<FeatureSeed>();
        //    //yield return context.Seed<PermissionGroupSeed>();
        //    //yield return context.Seed<PermissionGroupAccessAccessSeed>();

        //    //// Templates
        //    //yield return context.Seed<AutomationTemplateGroupSeed>();

        //    //// Wealth
        //    //yield return context.Seed<WealthReviewReasonTypeSeed>();
        //    //yield return context.Seed<FundEventTypeSeed>();
        //    //yield return context.Seed<MsFundMappingSeed>();
        //    //yield return context.Seed<WealthAssetClassSeed>();
            

        //}

        //public static IEnumerable<string> EnsureSeeded(this AuditContext context)
        //{
        //    _contextInit = () => new AuditContext(context._environmentConnection);

        //    // audit
        //    yield return context.Seed<AuditLevelSeed>();
        //    yield return context.Seed<AuditOperationSeed>();
        //}

        //public static IEnumerable<string> EnsureSeeded(this MailContext context)
        //{
        //    _contextInit = () => new MailContext(context._environmentConnection);

        //    // Email Sync
        //    yield return context.Seed<EmailServerTypeSeed>();
        //    yield return context.Seed<EmailServerSeed>();

        //}

        //public static string Seed<T>(this DbContext context) where T : ISeed
        //{
        //    try
        //    {
        //        var seeder = Activator.CreateInstance(typeof(T)) as ISeed;
        //        if (seeder == null) throw new NullReferenceException(nameof(seeder));
        //        var res = seeder.Seed(context);
        //        return res;
        //    }
        //    catch (Exception e)
        //    {
        //        //return $"Error seeding type { typeof(T).Name }: { e.FlattenMessages() }";
        //    }
        //}
    }
}

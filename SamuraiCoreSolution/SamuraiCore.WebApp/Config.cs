using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamuraiCore.WebApp
{
    public class Config
        : IConfig
    {
        #region Private Variables

        private readonly Configuration _config;

        #endregion

        #region Ctor

        internal Config()
        {
            _config = ConfigurationManager.OpenExeConfiguration(AssemblyDirectory);
            ConnectionStrings = new Dictionary<ContextName, string>
            {
                { ContextName.PrometheusBase, GetConectionString("SilverSurferBaseEntities")},
                { ContextName.LMS, GetConectionString("LeadManagementEntity")},
                { ContextName.Ram, GetConectionString("RAMServiceMessagesClientEntities")},
                { ContextName.TopBilling, GetConectionString("TopBilling")}
            };

            ServiceReferences = new Dictionary<string, string>
            {
                {"JBilling", GetSetting("JBilling.ServiceUri", string.Empty)},
                {"TopBilling", GetSetting("TopBilling.ServiceUri", string.Empty)},
                {"Hotspotter", GetSetting("Hotspotter.ServiceUri", string.Empty)},
                {"SOSSquad", GetSetting("SOSSquad.ServiceUri", string.Empty)},
                {"VasxViewBilling", GetSetting("VasxViewBilling.ServiceUri", string.Empty)},
                {"SafeBase", GetSetting("SafeBase.ServiceUri", string.Empty)},
                {"TCHService", GetSetting("TCHService.ServiceUri", string.Empty)},
                {"CarCure", GetSetting("CarCure.ServiceUri", string.Empty)},
                {"ExtremeLifestyle", GetSetting("ExtremeLifestyle.ServiceUri", string.Empty)},
                {"SafetyBox", GetSetting("SafetyBox.ServiceUri", string.Empty)},
                {"Simfy", GetSetting("Simfy.ServiceUri",string.Empty)},
                {"iOnline", GetSetting("iOnline.ServiceUri", string.Empty)},
                {"Magzone", GetSetting("Magzone.ServiceUri", string.Empty)},
                {"MrpVasx", GetSetting("MrpVasx.ServiceUri", string.Empty)},
                {"Society", GetSetting("Society.ServiceUri", string.Empty)},
                {"VasXUpgrade", GetSetting("VasXAPIV2.ServiceUri", string.Empty)},
                {"VasxView", GetSetting("VasxView.ServiceUri", string.Empty)}
            };

            AzureStorageConnection = GetSetting("AzureStorageConnection", string.Empty);
            StorageConnection = GetSetting("StorageConnection", string.Empty);
            CoreAddress = GetSetting("CoreAddress", string.Empty);
            BillingImportQueueName = GetSetting("BillingImportQueueName", string.Empty);
            DealUploadQueueName = GetSetting("DealUploadQueueName", string.Empty);

            JBillingApiUser = GetSetting("JBillingApiUser", string.Empty);
            JBillingUser = GetSetting("JBillingUser", string.Empty);
            JBillingPassword = GetSetting("JBillingPassword", string.Empty);

            CommsQueueName = GetSetting("CommsQueueName", string.Empty);
            ActionDrivenCommsQueueName = GetSetting("ActionDrivenCommsQueueName", string.Empty);
            ActorResurrectionQueueName = GetSetting("ActorResurrectionQueueName", string.Empty);

            BillingPaymentQueueName = GetSetting("BillingPaymentImportQueueName", string.Empty);
            BillingManualStorageName = GetSetting("BillingManualStorageName", string.Empty);
            BillingManualQueueName = GetSetting("BillingManualQueueName", string.Empty);
            BillingOnceOffStorageName = GetSetting("BillingOnceOffStorageName", string.Empty);
            BillingOnceOffQueueName = GetSetting("BillingOnceOffQueueName", string.Empty);
            BillingExtractQueueName = GetSetting("BillingExtractQueueName", string.Empty);
            ActivationProcessQueue = GetSetting("ActivationProcessingQueueName", string.Empty);
            RefreshQueueName = GetSetting("BillingRefreshQueueName", string.Empty);
            BillingUpdateQueueName = GetSetting("BillingUpdateQueueName", string.Empty);
            BillingErrorQueueName = GetSetting("BillingErrorQueueName", string.Empty);

            BillingSettings = GetSetting("BillingSettings", string.Empty);
            TopBillingUsername = GetSetting("TopBillingUsername", string.Empty);
            TopBillingPassword = GetSetting("TopBillingPassword", string.Empty);
            TopBillingServer = GetSetting("TopBillingServer", string.Empty);
            Module = GetSetting("Module", string.Empty);

            UsernamePrefix_DealDepot = GetSetting("UsernamePrefix_DealDepot", string.Empty);
            UsernamePrefix_FinesCure = GetSetting("UsernamePrefix_FinesCure", string.Empty);
            UsernamePrefix_Halo = GetSetting("UsernamePrefix_Halo", string.Empty);
            UsernamePrefix_Hotspotter = GetSetting("UsernamePrefix_Hotspotter", string.Empty);
            UsernamePrefix_LifeAid = GetSetting("UsernamePrefix_LifeAid", string.Empty);
            UsernamePrefix_Luno = GetSetting("UsernamePrefix_Luno", string.Empty);
            UsernamePrefix_Milc = GetSetting("UsernamePrefix_Milc", string.Empty);
            UsernamePrefix_SafeBase = GetSetting("UsernamePrefix_SafeBase", string.Empty);
            UsernamePrefix_Society = GetSetting("UsernamePrefix_Society", string.Empty);
            UsernamePrefix_SOSquad = GetSetting("UsernamePrefix_SOSquad", string.Empty);
            UsernamePrefixChase = GetSetting("UsernamePrefixChase", string.Empty);
            UsernamePrefixOnAir = GetSetting("UsernamePrefixOnAir", string.Empty);
            UsernamePrefix_Simfy = GetSetting("UsernamePrefix_Simfy", string.Empty);

            ThirdPartyUrl_Kintel = GetSetting("ThirdPartyUrl_Kintel", string.Empty);
            ThirdPartyUrl_PhoneBox = GetSetting("ThirdPartyUrl_PhoneBox", string.Empty);
            ThirdPartyUrl_TechTrace = GetSetting("ThirdPartyUrl_TechTrace", string.Empty);

            SMSEndpoint = GetSetting("SMSEndpoint", string.Empty);
            MandrillSendTemplateUrl = GetSetting("MandrillSendTemplateUrl", string.Empty);
            MandrillRenderTemplateUrl = GetSetting("MandrillRenderTemplateUrl", string.Empty);
            OrderReferenceTag = GetSetting("OrderReferenceTag", string.Empty);
            FirstNameTag = GetSetting("FirstNameTag", string.Empty);
            LastNameTag = GetSetting("LastNameTag", string.Empty);
            TinyURLTag = GetSetting("TinyURLTag", string.Empty);
            ActivationNumTag = GetSetting("ActivationNumTag", string.Empty);
            CustomerIdNumberTag = GetSetting("CustomerIdNumberTag", string.Empty);
            UsernameTag = GetSetting("UsernameTag", string.Empty);
            PasswordTag = GetSetting("PasswordTag", string.Empty);
            WaybillNumberTag = GetSetting("WaybillNumberTag", string.Empty);
            VoucherTag = GetSetting("VoucherTag", string.Empty);
            VoucherCodeTag = _config.AppSettings.Settings["VoucherCodeTag"].Value;
            VoucherAmountTag = _config.AppSettings.Settings["VoucherAmountTag"].Value;
            Testing = GetSetting("Testing", string.Empty);
            InstallerName = GetSetting("InstallerName", string.Empty);
            InstallerAddress = GetSetting("InstallerAddress", string.Empty);
            InstallerContactNumber = GetSetting("InstallerContactNumber", string.Empty);
            Brand = GetSetting("Brand", string.Empty);
            CarCurePassword = GetSetting("CarCurePassword", string.Empty);
            Environment = GetSetting("Environment", string.Empty);
            ClientName = GetSetting("ClientName", string.Empty);
            CallType = GetSetting("CallType", string.Empty);
            DealershipName = GetSetting("DealershipName", string.Empty);
            TermType = GetSetting("TermType", string.Empty);
            CarCureProductOptionName = GetSetting("CarCureProductOptionName", string.Empty);
            CarCureProductSuiteName = GetSetting("CarCureProductSuiteName", string.Empty);
            CustomerType = GetSetting("CustomerType", string.Empty);
            MagzoneZinionExternalRef = GetSetting("MagzoneZinionExternalRef", string.Empty);
            MagZoneZinioPrice = GetSetting("MagZoneZinioPrice", string.Empty);

            ThirdPartyUrl_Driven = GetSetting("ThirdPartyUrl_Driven", string.Empty);
            CreateMethodName_Driven = GetSetting("CreateMethodName_Driven", string.Empty);
            UsernamePrefix_Driven = GetSetting("UsernamePrefix_Driven", string.Empty);
            DebitCount = GetSetting("DebitCount", string.Empty);
            BillingOnceOffFileUploadDelimeter = GetSetting("BillingOnceOffFileUploadDelimeter", string.Empty);

            WaitByMinutes = GetSetting("WaitByMinutes", false);
            WaitTime = GetSetting("WaitTime", 4);
            ActivationWaitByMinutes = GetSetting("ActivationWaitByMinutes", true);
            ActivationWaitTime = GetSetting("ActivationWaitTime", 1);

            SafeBaseSuccessCode = GetSetting("SafeBaseSuccessCode", string.Empty);
            SafetyBoxSuccessCode = GetSetting("SafetyBoxSuccessCode", string.Empty);

            UseBulkStatusUploadBlockerForStatuses = GetSetting("UseBulkStatusUploadBlockerForStatuses", false);

            VoucherCodeTag = GetSetting("VoucherCodeTag", string.Empty);
            VoucherAmountTag = GetSetting("VoucherAmountTag", string.Empty);

            bool waitByMinutes;
            bool.TryParse(GetSetting("WaitByMinutes", string.Empty), out waitByMinutes);
            WaitByMinutes = waitByMinutes;

            int waitTime;
            var parsed = int.TryParse(GetSetting("WaitTime", string.Empty), out waitTime);
            WaitTime = (parsed) ? waitTime : 4;

            int maxDays;
            var billingMaxDays = int.TryParse(GetSetting("BillingFailedServiceMaxDays", string.Empty), out maxDays);
            BillingMaxDays = billingMaxDays ? maxDays : -5;


            int addressMaxLength;
            var addressMaxLengthParsed = int.TryParse(GetSetting("BillingAddressMaxLength", string.Empty), out addressMaxLength);
            BillingAddressMaxLength = (addressMaxLengthParsed) ? addressMaxLength : 100;

            int billingOnceOffFileUploadLength;
            var billingOnceOffFileUploadLengthParsed = int.TryParse(GetSetting("BillingOnceOffFileUploadLength", string.Empty), out billingOnceOffFileUploadLength);
            BillingOnceOffFileUploadLength = billingOnceOffFileUploadLengthParsed ? billingOnceOffFileUploadLength : 3;

            int billingManualFileUploadLength;
            var billingManualFileUploadLengthParsed = int.TryParse(GetSetting("BillingManualFileUploadLength", string.Empty), out billingManualFileUploadLength);
            BillingManualFileUploadLength = billingManualFileUploadLengthParsed ? billingManualFileUploadLength : 5;

            int onceOffId;
            var onceOffIdParsed = int.TryParse(GetSetting("OnceOffId", string.Empty), out onceOffId);
            OnceOffId = onceOffIdParsed ? onceOffId : 1;

            int methodId;
            var methodIdParsed = int.TryParse(GetSetting("MethodId", string.Empty), out methodId);
            MethodId = methodIdParsed ? methodId : -1;


            int topBillingTimeoutMinutes;
            parsed = int.TryParse(GetSetting("TopBillingTimeoutMinutes", string.Empty), out topBillingTimeoutMinutes);
            TopBillingTimeoutMinutes = (parsed) ? topBillingTimeoutMinutes : 5;

            int jBillingTimeoutMinutes;
            parsed = int.TryParse(GetSetting("JBillingTimeoutMinutes", string.Empty), out jBillingTimeoutMinutes);
            JBillingTimeoutMinutes = (parsed) ? jBillingTimeoutMinutes : 5;

            // Top Billing
            int maxBufferSize;
            parsed = int.TryParse(GetSetting("MaxBufferSize", string.Empty), out maxBufferSize);
            MaxBufferSize = (parsed) ? maxBufferSize : 268435456;

            int topBillingHoursToLookBack;
            parsed = int.TryParse(GetSetting("TopBillingHoursToLookBack", string.Empty), out topBillingHoursToLookBack);
            TopBillingHoursToLookBack = (parsed) ? topBillingHoursToLookBack : 1;


            long maxReceivedMessageSize;
            parsed = long.TryParse(GetSetting("MaxReceivedMessageSize", string.Empty), out maxReceivedMessageSize);
            MaxReceivedMessageSize = (parsed) ? maxReceivedMessageSize : 268435456;

            long maxBufferPoolSize;
            parsed = long.TryParse(GetSetting("MaxBufferPoolSize", string.Empty), out maxBufferPoolSize);
            MaxBufferPoolSize = (parsed) ? maxBufferPoolSize : 268435456;


            // For Logging to LogEntries
            EnvironmentType = GetSetting("EnvironmentType", string.Empty);
            LogEntriesIsEnabled = GetSetting("LogEntriesIsEnabled", false);

            // JBilling
            AccessEntity = GetSetting("AccessEntity", 62);
            BillingType = GetSetting("BillingType", 2);
            CurrencyId = GetSetting("CurrencyId", 60);
            UserAssignedStatusId = GetSetting("UserAssignedStatusId", 4);
            OrderStatusId = GetSetting("OrderStatusId", 4);

            // Billing run settings
            BillingWaitTime = GetSetting("BillingWaitTime", 12); // Hours
            BillingEndTime = GetSetting("BillingEndTime", string.Empty);
            MainRoleId = GetSetting("MainRoleId", 5);
            LanguageId = GetSetting("LanguageId", 1);
            CustomerStatusId = GetSetting("CustomerStatusId", 1);
            SubscriberStatusId = GetSetting("SubscriberStatusId", 1);

            ShowMaxBaseUrl = GetSetting("ShowMaxBaseUrl", string.Empty);
            ShowmaxPartnerId = GetSetting("ShowmaxPartnerId", string.Empty);
            ShowmaxAccessToken = GetSetting("ShowmaxAccessToken", string.Empty);

            OnAirTVBaseUrl = GetSetting("OnAirTVBaseURL", string.Empty);
            OnAirTVPartnerId = GetSetting("OnAirTVPartnerId", string.Empty);
            OnAirTVAccessToken = GetSetting("OnAirTVAccessToken", string.Empty);

            //VASX Actions
            VASXAccountCreationAction = GetSetting("VASXAccountCreationAction", string.Empty);
            VASXCreateAndActivateSubscriberAction = GetSetting("VASXCreateAndActivateSubscriberAction", string.Empty);
            VASXSuccessCode = GetSetting("VASXSuccessCode", string.Empty);
            VasxActionName = GetSetting("VasxActionName", string.Empty);

            //MRP Mobile Configs
            MrpMobileVasxUsername = GetSetting("MrpMobile.VASXUsername", string.Empty);
            MrpMobileMsisdnless = GetSetting("MrpMobile.Msisdnless", string.Empty);
            MrpMobileVasxPassword = GetSetting("MrpMobile.VASXPassword", string.Empty);
            MrpMobileAccountType = GetSetting("MrpMobile.VasxAccountType", string.Empty);
            MrpMobileDirectDebit = GetSetting("MrpMobile.DirectDebit", string.Empty);
            MrpMobileDeliveryMethod = GetSetting("MrpMobile.DeliveryMethod", string.Empty);
            MrpMobileAutoBarRule = GetSetting("MrpMobile.AutoBarRule", string.Empty);

            MrpMobileLimitNotifyLevel1 = GetSetting("MrpMobile.LimitNotifyLevel1", string.Empty);
            MrpMobileLimitNotifyLevel2 = GetSetting("MrpMobile.LimitNotifyLevel2", string.Empty);
            MrpMobileLimitNotifyLevel3 = GetSetting("MrpMobile.LimitNotifyLevel3", string.Empty);
            MrpMobileCreditTab = GetSetting("MrpMobile.CreditTab", string.Empty);

            //MRP Coporate
            MrpCorporateAccountType = GetSetting("MrpCorporate.VasxAccountType", string.Empty);

            //MRP SimOnly Configs
            MrpSimOnlyVasxUsername = GetSetting("MrpSimOnly.VASXUsername", string.Empty);
            MrpSimOnlyMsisdnless = GetSetting("MrpSimOnly.Msisdnless", string.Empty);
            MrpSimOnlyVasxPassword = GetSetting("MrpSimOnly.VASXPassword", string.Empty);
            MrpSimOnlyAutoBarRule = GetSetting("MrpSimOnly.AutoBarRule", string.Empty);
            MrpSimOnlyVasxAccountNumber = GetSetting("MrpSimOnly.VasxAccountNumber", string.Empty);
            MrpSimOnlyVasxAccountUid = GetSetting("MrpSimOnly.VasxAccountUID", string.Empty);
            MrpSimOnlySubscriberType = GetSetting("MrpSimOnly.SubscriberType", string.Empty);
            MrpSimOnlyLanguage = GetSetting("MrpSimOnly.Language", string.Empty);
            MrpSimOnlyCurrency = GetSetting("MrpSimOnly.Currency", string.Empty);
            MrpSimOnlyBillCycle = GetSetting("MrpSimOnly.BillCycle", string.Empty);

            MrpSimOnlyVasxVirtualNetwork = GetSetting("MrpSimOnly.VASXVirtualNetwork", 1);
            MrpSimOnlyCreditTab = GetSetting("MrpSimOnly.CreditTab", 3338);

            int mrpSimOnlyVasxVirtualNetwork;
            parsed = int.TryParse(GetSetting("MrpSimOnly.VASXVirtualNetwork", string.Empty), out mrpSimOnlyVasxVirtualNetwork);
            MrpSimOnlyVasxVirtualNetwork = (parsed) ? mrpSimOnlyVasxVirtualNetwork : 1;

            int mrpSimOnlyCreditTab;
            parsed = int.TryParse(GetSetting("MrpSimOnly.CreditTab", string.Empty), out mrpSimOnlyCreditTab);
            MrpSimOnlyCreditTab = (parsed) ? mrpSimOnlyCreditTab : 3338;

            //RedData Configs
            RedDataAutoBarRule = GetSetting("RedData.AutoBarRule", string.Empty);
            RedDataBillCycleUid = GetSetting("RedData.BillCycleUid", string.Empty);
            RedDataBillCycle = GetSetting("RedData.BillCycle", string.Empty);
            RedDataDeliveryMethod = GetSetting("RedData.DeliveryMethod", string.Empty);
            RedDataDirectDebit = GetSetting("RedData.DirectDebit", string.Empty);
            RedDataVasxPassword = GetSetting("RedData.VASXPassword", string.Empty);
            RedDataVasxUsername = GetSetting("RedData.VASXUsername", string.Empty);
            RedDataVasxAccountType = GetSetting("RedData.VasxAccountType", string.Empty);
            RedDataStatementType = GetSetting("RedData.StatementType", string.Empty);
            RedDataMsisdnless = GetSetting("RedData.Msisdnless", string.Empty);
            RedDataSubscriberType = GetSetting("RedData.SubscriberType", string.Empty);
            RedDataLanguage = GetSetting("RedData.Language", string.Empty);
            RedDataCurrency = GetSetting("RedData.Currency", string.Empty);

            int redDataVasxVirtualNetwork;
            parsed = int.TryParse(GetSetting("RedData.VASXVirtualNetwork", string.Empty), out redDataVasxVirtualNetwork);
            RedDataVasxVirtualNetwork = (parsed) ? redDataVasxVirtualNetwork : 12;

            int voucherTypeId;
            parsed = int.TryParse(GetSetting("Society.VoucherTypeId", string.Empty), out voucherTypeId);
            SocietyVoucherTypeId = (parsed) ? voucherTypeId : 4;

            ActivationProcessQueue = _config.AppSettings.Settings["ActivationProcessingQueueName"].Value;


            VasXSessionsSettings = _config.AppSettings.Settings["VasXAPIV2.Session"].Value;

            int mrpVasxVirtualNetwork;
            parsed = int.TryParse(_config.AppSettings.Settings["MrpMobile.VASXVirtualNetwork"].Value, out mrpVasxVirtualNetwork);
            MrpMobileVasxVirtualNetwork = (parsed) ? mrpVasxVirtualNetwork : 1;

            MrpMobileStatementType = GetSetting("MrpMobile.StatementType", 'S');

            int vasXUpgradeTimeoutMinutes;
            parsed = int.TryParse(_config.AppSettings.Settings["VasXUpgradeTimeoutMinutes"].Value, out vasXUpgradeTimeoutMinutes);
            VasXUpgradeTimeoutMinutes = (parsed) ? vasXUpgradeTimeoutMinutes : 5;

            int vasXUpgradeDealerUID;
            parsed = int.TryParse(_config.AppSettings.Settings["VasxUpgradeDealerUID"].Value, out vasXUpgradeDealerUID);
            VasXUpgradeDealerUID = (parsed) ? vasXUpgradeDealerUID : 1;

            VasXUpgradesSearchType = _config.AppSettings.Settings["VasXUpgrades.SearchType"].Value;
            VasXAPIV2MsisdnPrefix = _config.AppSettings.Settings["VasXAPIV2.MsisdnPrefix"].Value;

            YelloDataCarrier = _config.AppSettings.Settings["YelloDataCarrier"].Value;

            SMSBulkImportQueueName = _config.AppSettings.Settings["SMSBulkImportQueueName"].Value;
            SMSBulkUploadBlobContainerName = _config.AppSettings.Settings["SMSBulkUploadBlobContainerName"].Value;
            SMSBulkUploadDelimeter = _config.AppSettings.Settings["SMSBulkUploadDelimeter"].Value;
            OrderReferenceDelimeter = _config.AppSettings.Settings["OrderReferenceDelimeter"].Value;

            DealUploadBlobName = GetSetting("DealUploadBlobName", "dealuploadblob");

            //Sale Pending Processing service configs
            int salePendingProcessingDelay_InMinutes;
            parsed = int.TryParse(_config.AppSettings.Settings["SalePendingProcessingDelay_InMinutes"].Value, out salePendingProcessingDelay_InMinutes);
            SalePendingProcessingDelay_InMinutes = (parsed) ? salePendingProcessingDelay_InMinutes : 30;

            int salePendingOrdersOlderThan_InHours;
            parsed = int.TryParse(_config.AppSettings.Settings["SalePendingOrdersOlderThan_InHours"].Value, out salePendingOrdersOlderThan_InHours);
            SalePendingOrdersOlderThan_InHours = (parsed) ? salePendingOrdersOlderThan_InHours : 1;

            int salePendingOrdersGoingBack_InDays;
            parsed = int.TryParse(_config.AppSettings.Settings["SalePendingOrdersGoingBack_InDays"].Value, out salePendingOrdersGoingBack_InDays);
            SalePendingOrdersGoingBack_InDays = (parsed) ? salePendingOrdersGoingBack_InDays : 1;

            int salePendingOrdersRecordstoRetrieveFromDB;
            parsed = int.TryParse(_config.AppSettings.Settings["SalePendingOrdersRecordstoRetrieveFromDB"].Value, out salePendingOrdersRecordstoRetrieveFromDB);
            SalePendingOrdersRecordstoRetrieveFromDB = (parsed) ? salePendingOrdersRecordstoRetrieveFromDB : 100;


            parsed = int.TryParse(GetSetting("Techlocker.VoucherTypeId", string.Empty), out voucherTypeId);
            TechlockerVoucherTypeId = (parsed) ? voucherTypeId : 56;

            VoucherSystemBaseURL = GetSetting("VoucherSystemBaseURL", "http://vouchers.admin.testing/modules/vasVouchers/api/");

            //AtMail
            AtMailServiceUrl = _config.AppSettings.Settings["AtMailServiceUrl"].Value;
            AtMailAuthKey = _config.AppSettings.Settings["AtMailAuthKey"].Value;
            AtMailFailedStatus = _config.AppSettings.Settings["AtMailFailedStatus"].Value;
            AtMailExistKeyword = _config.AppSettings.Settings["AtMailExistKeyword"].Value;
            AtMailDefaultPassword = _config.AppSettings.Settings["AtMailDefaultPassword"].Value;

            //Viva life Email Domain
            VivaEmailDomain = _config.AppSettings.Settings["VivaEmailDomain"].Value;

            // vas Create subscription 
            OrderItemNumber = _config.AppSettings.Settings["OrderItemNumber"].Value;
            ItemEscalation = _config.AppSettings.Settings["ItemEscalation"].Value;
            CallFlag = _config.AppSettings.Settings["CallFlag"].Value;
            UserNameCreateSubscription = _config.AppSettings.Settings["UserNameCreateSubscription"].Value;

            FraudCheckUploadQueueName = GetSetting("FraudCheckUploadQueueName", "fraudcheckqueue");
            FraudCheckUploadBlobName = GetSetting("FraudCheckUploadBlobName", "fraudcheckuploadblob");
            FraudCheckUploadDelimeter = GetSetting("FraudCheckUploadDelimeter", ",");

            int fraudCheckIdentifierMaxLength;
            parsed = int.TryParse(_config.AppSettings.Settings["FraudCheckIdentifierNumberMaxLength"].Value, out fraudCheckIdentifierMaxLength);
            FraudCheckIdentifierNumberMaxLength = (parsed) ? fraudCheckIdentifierMaxLength : 30;

            int fraudCheckMSISDNMaxLength;
            parsed = int.TryParse(_config.AppSettings.Settings["FraudCheckMSISDNMaxLength"].Value, out fraudCheckMSISDNMaxLength);
            FraudCheckMSISDNMaxLength = (parsed) ? fraudCheckMSISDNMaxLength : 50;

            int fraudCheckBankAccountMaxLength;
            parsed = int.TryParse(_config.AppSettings.Settings["FraudCheckBankAccoutMaxLength"].Value, out fraudCheckBankAccountMaxLength);
            FraudCheckBankAccoutMaxLength = (parsed) ? fraudCheckBankAccountMaxLength : 50;

            BulkProductUploadQueueName = GetSetting("BulkProductUploadQueueName", "bulkproductuploadq");
            BulkProductUploadBlobName = GetSetting("BulkProductUploadBlobName", "bulkproductuploadblob");

            InfraScaleHeaderUsername = GetSetting("InfraScaleHeaderUsername", "32631WS");
            InfraScaleHeaderPassword = GetSetting("InfraScaleHeaderPassword", "uY8zKVz");
            InfraScaleBodyUsername = GetSetting("InfraScaleBodyUsername", "Ign1t10n");
            InfraScaleBodyPassword = GetSetting("InfraScaleBodyPassword", "Ignition");
            InfraScaleCustomerTypeCode = GetSetting("InfraScaleCustomerTypeCode", "32631WS");
            InfraScaleSecretQuestion = GetSetting("InfraScaleSecretQuestion", "en_GB");
            InfraScalelocaleCode = GetSetting("InfraScalelocaleCode", "What is your Id Number?");

            //VAS Basket Limit
            int vasBasketLimit;
            parsed = int.TryParse(GetSetting("VASBasketLimit", string.Empty), out vasBasketLimit);
            VASBasketLimit = (parsed) ? vasBasketLimit : 5;
            int vasBasketLimitMonthsCheck;
            parsed = int.TryParse(GetSetting("VASBasketLimitMonthsCheck", string.Empty), out vasBasketLimitMonthsCheck);
            VASBasketLimitMonthsCheck = (parsed) ? vasBasketLimitMonthsCheck : -6;
            SimfyBaseUrl = GetSetting("SimfyBaseUrl", "https://simfyafrica.staging.exactdev.co.za/ApiZa/user");
            SimfyApiUsername = GetSetting("SimfyApiUsername", "ign");
            SimfyApiPassword = GetSetting("SimfyApiPassword", "ign");
            SimfyEmail = GetSetting("SimfyEmail", "@somusic.co.za");
            SimfyLogging = GetSetting("SimfyLogging", "false");

            //AVSR
            AvsrCertLocation = GetSetting("certlocation", @"C:\IgnDev\SilverSurfer\DotNetSystem\Prometheus.Core\Prometheus.Core.WebService\Certificates\avsr_ignitiongroup.p12");
            AvsrCertPassword = GetSetting("certpassword", "avsruat");
            AvsrUser = GetSetting("avsruser", "AVSR004000");

            //TCH
            TchApiUsername = GetSetting("TchApiUsername", "admin");
            TchApiPassword = GetSetting("TchApiPassword", "C0ffee72!");
            TchCreateFunction = GetSetting("TchCreateFunction", "create_user");
            TchCancelFunction = GetSetting("TchCancelFunction", "cancel_user");
            TchActivateFunction = GetSetting("TchActivateFunction", "activate_user");

            bool isTesting;
            bool.TryParse(GetSetting("ThirdPartyServiceIsTesting", string.Empty), out isTesting);
            ThirdPartyServiceIsTesting = isTesting;


            // Zinio API method URL's
            ZinioBaseUrl = GetSetting("ZinioBaseUrl", "https://sbx-api-sec.ziniopro.com");
            ZinioTokenAuthAppendedUrl = GetSetting("ZinioTokenAuthAppendedUrl", "oauth/v2/tokens");
            ZinioCreateSubscriptionAppendedUrl = GetSetting("ZinioCreateSubscriptionAppendedUrl", "fulfillment/v2/label_batches");
            ZinioUserCreationAppendedUrl = GetSetting("ZinioUserCreationAppendedUrl", "identity/v2/users");


            // Zinio Token Authentication

            ZinioClientId = GetSetting("ZinioClientId", "7633e12d974a4c9c9c0a5b80a9637f16");
            ZinioClientSecret = GetSetting("ZinioClientSecret", "23400e68acf941aca88aa1a409ede8b9");
            ZinioGrantType = GetSetting("ZinioGrantType", "client_credentials");

            // Zinio Create User
            ZinioDirectoryId = GetSetting("ZinioDirectoryId", 3);
            ZinioRegistrationStatus = GetSetting("ZinioRegistrationStatus", 0);
            ZinioSendPreregistrationEmail = GetSetting("ZinioSendPreregistrationEmail", true);
            ZinioStatus = GetSetting("ZinioStatus", 1);
            ZinioCreatedBy = GetSetting("ZinioCreatedBy", 6251);
            ZinioCreatedByType = GetSetting("ZinioCreatedByType", 2);

            // Zinio Get User
            ZinioDirectory = GetSetting("ZinioDirectory", "directory_id");
            ZinioNewsStands = GetSetting("ZinioNewsStands", "newsstand/v2/newsstands");
            ZinioUserRegistrations = GetSetting("ZinioUserRegistrations", "user_registrations");
            ZinioNewsStandId = GetSetting("ZinioNewsStandId", 134);

            // Zinio Create Subscription
            ZinioProjectId = GetSetting("ZinioProjectId", 99);
            ZinioAccountId = GetSetting("ZinioAccountId", 6251);
            ZinioSourceApplicationId = GetSetting("ZinioSourceApplicationId", 11391);
            ZinioSourceProjectId = GetSetting("ZinioSourceProjectId", 1139);
            ZinioProductType = GetSetting("ZinioProductType", 3);
            ZinioPublicationIds = GetSetting("ZinioPublicationIds",
                "8631,6654,1442,5291,7885,5181,1656,1657,1186,4284,7756,7623,2615,5132,6302,2277,1623,2810,4266,1569,1135,3210,1658,1187,2861,1330,2449,1331,8633,5148,7882,1624,5133,4876,4896,7691,7088,6363,8634,6146,5186,7883,3209,5155,6932,1592,5187,1564,8063,4812,8084,5976,2117,7624,4613,8353,7762,1593,4227,5188,5345,1594,6665,4128,33503,6977");
            DefaultServiceTimeoutMinutes = GetSetting("DefaultServiceTimeoutMinutes", 5);
            int takeCountForTempUpload;
            parsed = int.TryParse(_config.AppSettings.Settings["TakeCountForTempUpload"].Value, out takeCountForTempUpload);
            TakeCountForTempUpload = (parsed) ? takeCountForTempUpload : 50;

            double waitTimeForTempUpload;
            parsed = double.TryParse(_config.AppSettings.Settings["WaitTimeInMinutesForTempUpload"].Value, out waitTimeForTempUpload);
            WaitTimeInMinutesForTempUpload = (parsed) ? waitTimeForTempUpload : 5;

            TempLeadFileUploadBlobName = GetSetting("TempLeadFileUploadBlobName", "leadblobqueue");
            TempLeadFileUploadQueueName = GetSetting("TempLeadFileUploadQueueName", "leadfileblobqueue");
            LeadUploadDelimeter = GetSetting("LeadUploadDelimeter", ",");

            double waitTimeForLeadBlob;
            parsed = double.TryParse(_config.AppSettings.Settings["WaitTimeInMinutesForLeadBlobService"].Value, out waitTimeForLeadBlob);
            WaitTimeInMinutesForLeadBlobService = (parsed) ? waitTimeForLeadBlob : 15;

            double waitTimeForLeadFTP;
            parsed = double.TryParse(_config.AppSettings.Settings["WaitTimeInMinutesForLeadFTPService"].Value, out waitTimeForLeadFTP);
            WaitTimeInMinutesForLeadFTPService = (parsed) ? waitTimeForLeadFTP : 15;

            SalePendingQueueName = GetSetting("SalePendingQueueName", "salependingqueue");

            // AlwaysOn Third Party
            AlwaysOnApiKey = GetSetting("AlwaysOnApiKey", "R5g7NYy-19903Dv6HqPf-30259GqqLrC");
            AlwaysOnBaseUrl = GetSetting("AlwaysOnBaseUrl", "https://hotspot.alwayson.co.za/AlwaysOnVoucherAPI/VoucherService.svc");
            AlwaysOn_Method_CreateVoucherCustomUserName = GetSetting("AlwaysOn_Method_CreateVoucherCustomUserName", "CreateVoucherCustomUsername");
            AlwaysOn_Method_CreateCustomer = GetSetting("AlwaysOn_Method_CreateCustomer", "CreateCustomer");
            AlwaysOn_Method_CancelVoucher = GetSetting("AlwaysOn_Method_CancelVoucher", "CancelVoucher");
            AlwaysOn_Method_GetLocations = GetSetting("AlwaysOn_Method_GetLocations", "getHotspotLocations");
            AlwaysOn_Method_CheckBalance = GetSetting("AlwaysOn_Method_CheckBalance", "GetVoucherUsageSummary");
            AlwaysOn_Method_ChangePassword = GetSetting("AlwaysOn_Method_ChangePassword", "ChangePassword");
            AlwaysOn_Param_ApiKey = GetSetting("AlwaysOn_Param_ApiKey", "api_key");
            AlwaysOn_Param_UserName = GetSetting("AlwaysOn_Param_UserName", "username");
            AlwaysOn_Param_Name = GetSetting("AlwaysOn_Param_Name", "name");
            AlwaysOn_Param_SurName = GetSetting("AlwaysOn_Param_SurName", "surname");
            AlwaysOn_Param_Password = GetSetting("AlwaysOn_Param_Password", "password");
            AlwaysOn_Param_Mobile = GetSetting("AlwaysOn_Param_Mobile", "mobile");
            AlwaysOn_Param_VoucherCode = GetSetting("AlwaysOn_Param_VoucherCode", "vouchercode");
            AlwaysOn_Param_Email = GetSetting("AlwaysOn_Param_Email", "email");
            AlwaysOn_Param_CellPhone = GetSetting("AlwaysOn_Param_CellPhone", "cellphone");
            AlwaysOn_Param_PackageId = GetSetting("AlwaysOn_Param_PackageId", "package_id");
            AlwaysOn_Param_SingleCode = GetSetting("AlwaysOn_Param_SingleCode", "single_code");
            AlwaysOn_Param_Lata = GetSetting("AlwaysOn_Param_Lata", "lata");
            AlwaysOn_Param_Latb = GetSetting("AlwaysOn_Param_Latb", "latb");
            AlwaysOn_Param_Lnga = GetSetting("AlwaysOn_Param_Lnga", "lnga");
            AlwaysOn_Param_Lngb = GetSetting("AlwaysOn_Param_Lngb", "lngb");

            //ClickaTell
            ClickatellBaseUrl = GetSetting("ClickatellBaseUrl", "http://api.clickatell.com/http/sendmsg");
            ClickatellApiUsername = GetSetting("ClickatellApiUsername", "comit");
            ClickatellApiPassword = GetSetting("ClickatellApiPassword", "bergsma777");
            ClickatellApiId = GetSetting("ClickatellApiId", "1059733");
            SocietyVoucherURL = GetSetting("SocietyVoucherURL", "http://vouchers.admin.testing/modules/vasVouchers/api/");

            bool clickatellIsTesting;
            bool.TryParse(GetSetting("ClickatellIsTesting", string.Empty), out clickatellIsTesting);
            ClickatellIsTesting = clickatellIsTesting;

            int _CleansingComit;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.Comit"].Value, out _CleansingComit);
            CleansingComit = (parsed) ? _CleansingComit : 385;

            int _CleansingVoice;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.Voice"].Value, out _CleansingVoice);
            CleansingVoice = (parsed) ? _CleansingVoice : 384;

            int _CleansingComitDays;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.ComitDays"].Value, out _CleansingComitDays);
            CleansingComitDays = (parsed) ? _CleansingComitDays : 4;

            int _CleansingOtherDays;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.OtherDays"].Value, out _CleansingOtherDays);
            CleansingOtherDays = (parsed) ? _CleansingOtherDays : -28;

            int _CleansingTwoWeekPeriod;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.TwoWeekPeriod"].Value, out _CleansingTwoWeekPeriod);
            CleansingTwoWeekPeriod = (parsed) ? _CleansingTwoWeekPeriod : -15;

            int _CleansingCoolOffDate;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.CoolOffDate"].Value, out _CleansingCoolOffDate);
            CleansingCoolOffDate = (parsed) ? _CleansingCoolOffDate : 60;

            int _CleansingSalesPeriod;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.SalesPeriod"].Value, out _CleansingSalesPeriod);
            CleansingSalesPeriod = (parsed) ? _CleansingSalesPeriod : -15;

            int _CleansingRecheckPeriod;
            parsed = int.TryParse(_config.AppSettings.Settings["Cleansing.RecheckPeriod"].Value, out _CleansingRecheckPeriod);
            CleansingRecheckPeriod = (parsed) ? _CleansingRecheckPeriod : -30;

            int _RecycledLeadStatusDetailId;
            parsed = int.TryParse(_config.AppSettings.Settings["RecycledLeadStatusDetailId"].Value, out _RecycledLeadStatusDetailId);
            RecycledLeadStatusDetailId = (parsed) ? _RecycledLeadStatusDetailId : 1;

            int _RecycledLeadTypeId;
            parsed = int.TryParse(_config.AppSettings.Settings["RecycledLeadTypeId"].Value, out _RecycledLeadTypeId);
            RecycledLeadTypeId = (parsed) ? _RecycledLeadTypeId : 2;

            //debicheck
            DebiCheckAuthUrl = GetSetting("DebiCheckAuthUrl", "https://mercurius-uat.cib.digital/api/User/Authenticate");
            DebiCheckMandateUrl = GetSetting("DebiCheckMandateUrl", "https://mercurius-uat.cib.digital/api/initiation");
            DebiCheckListMandateUrl = GetSetting("DebiCheckListMandateUrl", "https://mercurius-uat.cib.digital/api/Mandate/List");
            DebiCheckAPIKey = GetSetting("DebiCheckAPIKey", "796B947E-FA77-4C81-B0CE-5E6950EF4C43");
            DebiCheckUsername = GetSetting("DebiCheckUsername", "ComitTech");
            DebiCheckPassword = GetSetting("DebiCheckPassword", "C0mi7T3ch@");
            DebicheckMandateStatus = GetSetting("DebicheckMandateStatus", "0,5,10,20,30,40");
            int debicheckMaxRetry;
            parsed = int.TryParse(GetSetting("DebiCheckMaxRetry", string.Empty), out debicheckMaxRetry);
            DebiCheckMaxRetry = (parsed) ? debicheckMaxRetry : 4;
            AthenticationInstrument = GetSetting("AthenticationInstrument", "0227");
            AuthenticationType = GetSetting("AuthenticationType", "10");
            CreditorAccountNumber = GetSetting("CreditorAccountNumber", "4047957501");
            CreditorPhoneNumber = GetSetting("CreditorPhoneNumber", "+27-315828352");
            CreditorEmail = GetSetting("CreditorEmail", "");
            Shortname = GetSetting("Shortname", "VAS");
            CollectionType = GetSetting("CollectionType", "AC");
            EntryClassCode = GetSetting("EntryClassCode", "32");
            DebitValueType = GetSetting("DebitValueType", "1");
            TrackingIndicator = GetSetting("TrackingIndicator", "true");
            DateAdjustmentAllowed = GetSetting("DateAdjustmentAllowed", "true");
            CurrencyCode = GetSetting("CurrencyCode", "ZAR");
            DebicheckQueueName = GetSetting("DebicheckQueueName", "debicheck");
            DebicheckCertLocation = GetSetting("DebicheckCertLocation", @"c:\Temp");
            DebicheckCertKey = GetSetting("DebicheckCertKey", "Igni123");
            int debicheckExpireMinutes;
            parsed = int.TryParse(_config.AppSettings.Settings["DebicheckSessionExpireInMinutes"].Value, out debicheckExpireMinutes);
            DebicheckSessionExpireInMinutes = (parsed) ? debicheckExpireMinutes : 60;

            int debicheckMaxDaysToProcessOrders;
            parsed = int.TryParse(_config.AppSettings.Settings["DebicheckMaxDaysToProcessOrders"].Value, out debicheckMaxDaysToProcessOrders);
            DebicheckMaxDaysToProcessOrders = (parsed) ? debicheckMaxDaysToProcessOrders : 4;


            TinyBaseURL = GetSetting("TinyBaseURL", "http://i.eoco.co.za/create.php?u=");

            BulkStatusUploadBlob = GetSetting("BulkStatusUploadBlob", "bulkstatusuploadblob");
            LeadCleansingUploadBlob = GetSetting("LeadCleansingUploadBlob", "leadcleansinguploadblob");

            // Driven
            DrivenPartnerCode = GetSetting("DrivenPartnerCode", "DRIVEN");
            DrivenPassword = GetSetting("DrivenPassword", "pR$54");
            DrivenWebServiceType = GetSetting("DrivenWebServiceType", "Member");
            DrivenOperation = GetSetting("DrivenOperation", "Create");

            MinPasswordLength = GetSetting("MinPasswordLength", 6);
        }

        #endregion

        #region Properties

        public string AzureStorageConnection { get; set; }
        public Dictionary<ContextName, string> ConnectionStrings { get; set; }
        public Dictionary<string, string> ServiceReferences { get; set; }
        public string StorageConnection { get; set; }

        public string DealUploadQueueName { get; set; }
        public string CommsQueueName { get; set; }
        public string ActionDrivenCommsQueueName { get; set; }
        public int TopBillingTimeoutMinutes { get; set; }
        public int JBillingTimeoutMinutes { get; set; }
        public string BillingOnceOffFileUploadDelimeter { get; set; }
        public int BillingOnceOffFileUploadLength { get; set; }
        public int BillingManualFileUploadLength { get; set; }
        public int BillingMaxDays { get; set; }
        public int BillingAddressMaxLength { get; set; }
        public string BillingImportQueueName { get; set; }
        public string BillingPaymentQueueName { get; set; }
        public string BillingExtractQueueName { get; set; }
        public string RefreshQueueName { get; set; }
        public string MagzoneZinionExternalRef { get; set; }
        public string MagZoneZinioPrice { get; set; }
        public string BillingUpdateQueueName { get; set; }
        public string BillingOnceOffQueueName { get; set; }
        public string BillingOnceOffStorageName { get; set; }
        public string BillingManualQueueName { get; set; }
        public string BillingManualStorageName { get; set; }
        public string BillingErrorQueueName { get; set; }
        public string TopBillingUsername { get; set; }
        public string TopBillingPassword { get; set; }
        public string TopBillingServer { get; set; }
        public string BillingSettings { get; set; }
        public string Module { get; set; }
        public string UsernamePrefix_DealDepot { get; set; }
        public string UsernamePrefix_FinesCure { get; set; }
        public string UsernamePrefix_Halo { get; set; }
        public string UsernamePrefix_Hotspotter { get; set; }
        public string UsernamePrefix_LifeAid { get; set; }
        public string UsernamePrefix_Luno { get; set; }
        public string UsernamePrefix_Milc { get; set; }
        public string UsernamePrefix_SafeBase { get; set; }
        public string UsernamePrefix_Society { get; set; }
        public string UsernamePrefix_SOSquad { get; set; }
        public string UsernamePrefix_Simfy { get; set; }


        public string ThirdPartyUrl_Kintel { get; set; }
        public string ThirdPartyUrl_PhoneBox { get; set; }
        public string ThirdPartyUrl_TechTrace { get; set; }

        public string SMSEndpoint { get; set; }
        public string MandrillSendTemplateUrl { get; set; }
        public string MandrillRenderTemplateUrl { get; set; }
        public string OrderReferenceTag { get; set; }
        public string FirstNameTag { get; set; }
        public string LastNameTag { get; set; }
        public string TinyURLTag { get; set; }
        public string ActivationNumTag { get; set; }
        public string CustomerIdNumberTag { get; set; }
        public string UsernameTag { get; set; }
        public string PasswordTag { get; set; }
        public string WaybillNumberTag { get; set; }
        public string VoucherTag { get; set; }
        public string VoucherCodeTag { get; set; }
        public string VoucherAmountTag { get; set; }
        public string Testing { get; set; }
        public string InstallerName { get; set; }
        public string InstallerAddress { get; set; }
        public string InstallerContactNumber { get; set; }
        public string Brand { get; set; }
        public string Environment { get; set; }
        public string CarCurePassword { get; set; }
        public string ClientName { get; set; }
        public string CallType { get; set; }
        public string DealershipName { get; set; }
        public string TermType { get; set; }
        public string CarCureProductSuiteName { get; set; }
        public string CarCureProductOptionName { get; set; }
        public string CustomerType { get; set; }
        public string CoreAddress { get; private set; }

        public bool WaitByMinutes { get; set; }

        public int WaitTime { get; set; }
        public bool ActivationWaitByMinutes { get; set; }

        public int ActivationWaitTime { get; set; }
        public string UsernamePrefixChase { get; set; }
        public string UsernamePrefixOnAir { get; set; }
        public string ActivationProcessQueue { get; set; }
        public string SafeBaseSuccessCode { get; set; }
        public string SafetyBoxSuccessCode { get; set; }
        public string EnvironmentType { get; set; }
        public bool LogEntriesIsEnabled { get; set; }

        public string UsernamePrefix_Driven { get; set; }
        public string ThirdPartyUrl_Driven { get; set; }
        public string CreateMethodName_Driven { get; set; }
        public string DebitCount { get; set; }

        public string JBillingPassword { get; set; }
        public string JBillingUser { get; set; }
        public string JBillingApiUser { get; set; }

        public int AccessEntity { get; set; }
        public int BillingType { get; set; }
        public int CurrencyId { get; set; }
        public int UserAssignedStatusId { get; set; }
        public int OrderStatusId { get; set; }
        public int MethodId { get; set; }

        public int MainRoleId { get; set; }
        public int LanguageId { get; set; }
        public int CustomerStatusId { get; set; }
        public int SubscriberStatusId { get; set; }
        public int OnceOffId { get; set; }

        public string SocietyVoucherURL { get; set; }

        public string ShowMaxBaseUrl { get; set; }
        public string ShowmaxPartnerId { get; set; }
        public string ShowmaxAccessToken { get; set; }

        public string OnAirTVBaseUrl { get; set; }
        public string OnAirTVPartnerId { get; set; }
        public string OnAirTVAccessToken { get; set; }

        public string ActorResurrectionQueueName { get; set; }

        public int BillingWaitTime { get; set; }
        public string BillingEndTime { get; set; }

        public string MrpMobileCreditTab { get; set; }
        public string VasxActionName { get; set; }
        public string VASXAccountCreationAction { get; set; }
        public string VASXCreateAndActivateSubscriberAction { get; set; }
        public string VASXSuccessCode { get; set; }

        public bool UseBulkStatusUploadBlockerForStatuses { get; set; }

        //Mrp Moblie Keys
        public string MrpMobileVasxUsername { get; set; }
        public string MrpMobileVasxPassword { get; set; }
        public int MrpMobileVasxVirtualNetwork { get; set; }
        public string MrpMobileAutoBarRule { get; set; }
        public string MrpMobileDirectDebit { get; set; }
        public string MrpMobileDeliveryMethod { get; set; }
        public string MrpMobileAccountType { get; set; }
        public char MrpMobileStatementType { get; set; }
        public string MrpMobileLimitNotifyLevel1 { get; set; }
        public string MrpMobileLimitNotifyLevel2 { get; set; }
        public string MrpMobileLimitNotifyLevel3 { get; set; }
        public string MrpMobileMsisdnless { get; set; }
        public string MrpCorporateAccountType { get; set; }
        public string VasXSessionsSettings { get; set; }

        //Mrp SimOnly Keys
        public string MrpSimOnlyVasxUsername { get; set; }
        public string MrpSimOnlyVasxPassword { get; set; }
        public int MrpSimOnlyVasxVirtualNetwork { get; set; }
        public string MrpSimOnlyAutoBarRule { get; set; }
        public string MrpSimOnlyMsisdnless { get; set; }
        public string MrpSimOnlyVasxAccountNumber { get; set; }
        public string MrpSimOnlyVasxAccountUid { get; set; }
        public int MrpSimOnlyCreditTab { get; set; }
        public string MrpSimOnlySubscriberType { get; set; }
        public string MrpSimOnlyLanguage { get; set; }
        public string MrpSimOnlyCurrency { get; set; }
        public string MrpSimOnlyBillCycle { get; set; }

        //RedData Keys
        public string RedDataVasxUsername { get; set; }
        public string RedDataVasxPassword { get; set; }
        public int RedDataVasxVirtualNetwork { get; set; }
        public string RedDataAutoBarRule { get; set; }
        public string RedDataBillCycleUid { get; set; }
        public string RedDataDirectDebit { get; set; }
        public string RedDataDeliveryMethod { get; set; }
        public string RedDataVasxAccountType { get; set; }
        public string RedDataStatementType { get; set; }
        public string RedDataMsisdnless { get; set; }
        public string RedDataBillCycle { get; set; }
        public string RedDataSubscriberType { get; set; }
        public string RedDataLanguage { get; set; }
        public string RedDataCurrency { get; set; }
        public int MaxBufferSize { get; set; }
        public long MaxReceivedMessageSize { get; set; }
        public long MaxBufferPoolSize { get; set; }
        public int TopBillingHoursToLookBack { get; set; }
        public int SocietyVoucherTypeId { get; set; }

        public int VasXUpgradeTimeoutMinutes { get; set; }

        public int VasXUpgradeDealerUID { get; set; }
        public string VasXUpgradesSearchType { get; set; }
        public string VasXAPIV2MsisdnPrefix { get; set; }
        public string YelloDataCarrier { get; set; }

        public string DealUploadBlobName { get; set; }

        public string SMSBulkImportQueueName { get; set; }

        public string SMSBulkUploadBlobContainerName { get; set; }

        public string SMSBulkUploadDelimeter { get; set; }
        public string OrderReferenceDelimeter { get; set; }
        // Sale Pending Processing Service settings
        //Get Orders in Sale Pending Older than **SalePendingOrdersOlderThan_InHours** Hours 
        //going back **SalePendingOrdersGoingBack_InDays** with a Third Party reference
        public int SalePendingProcessingDelay_InMinutes { get; set; }
        public int SalePendingOrdersOlderThan_InHours { get; set; }
        public int SalePendingOrdersGoingBack_InDays { get; set; }
        public int SalePendingOrdersRecordstoRetrieveFromDB { get; set; }

        public int TechlockerVoucherTypeId { get; set; }
        public string VoucherSystemBaseURL { get; set; }

        //AtMail Configs
        public string AtMailServiceUrl { get; set; }
        public string AtMailAuthKey { get; set; }
        public string AtMailFailedStatus { get; set; }
        public string AtMailExistKeyword { get; set; }
        public string AtMailDefaultPassword { get; set; }

        //Viva life Email Domain
        public string VivaEmailDomain { get; set; }

        // vas Create subscription 
        public string OrderItemNumber { get; set; }
        public string ItemEscalation { get; set; }
        public string CallFlag { get; set; }
        public string UserNameCreateSubscription { get; set; }
        public string FraudCheckUploadQueueName { get; set; }

        public string FraudCheckUploadBlobName { get; set; }

        public string FraudCheckUploadDelimeter { get; set; }

        public int FraudCheckIdentifierNumberMaxLength { get; set; }

        public int FraudCheckMSISDNMaxLength { get; set; }

        public int FraudCheckBankAccoutMaxLength { get; set; }

        public string BulkProductUploadQueueName { get; set; }

        public string BulkProductUploadBlobName { get; set; }
        public string InfraScaleHeaderUsername { get; set; }
        public string InfraScaleHeaderPassword { get; set; }
        public string InfraScaleBodyUsername { get; set; }
        public string InfraScaleBodyPassword { get; set; }
        public string InfraScaleCustomerTypeCode { get; set; }
        public string InfraScaleSecretQuestion { get; set; }
        public string InfraScalelocaleCode { get; set; }

        //VAS Basket Limit Settings
        public int VASBasketLimit { get; set; }
        public int VASBasketLimitMonthsCheck { get; set; }
        public string SimfyBaseUrl { get; set; }
        public string SimfyApiUsername { get; set; }
        public string SimfyApiPassword { get; set; }
        public string SimfyEmail { get; set; }
        public string SimfyLogging { get; set; }

        //Zinio API URL's
        public string ZinioBaseUrl { get; set; }

        public string ZinioTokenAuthAppendedUrl { get; set; }
        public string ZinioCreateSubscriptionAppendedUrl { get; set; }
        public string ZinioUserCreationAppendedUrl { get; set; }

        //Zinio Token Authentication
        public string ZinioGrantType { get; set; }
        public string ZinioClientId { get; set; }
        public string ZinioClientSecret { get; set; }

        // Zinio create User
        public int ZinioDirectoryId { get; set; }
        public int ZinioRegistrationStatus { get; set; }
        public bool ZinioSendPreregistrationEmail { get; set; }
        public int ZinioStatus { get; set; }
        public int ZinioCreatedBy { get; set; }
        public int ZinioCreatedByType { get; set; }

        // Zinio get User

        public string ZinioDirectory { get; set; }
        public string ZinioNewsStands { get; set; }
        public string ZinioUserRegistrations { get; set; }
        public int ZinioNewsStandId { get; set; }

        public int DefaultServiceTimeoutMinutes { get; set; }

        // Zinio create subscription
        public int ZinioProjectId { get; set; }
        public int ZinioAccountId { get; set; }
        public int ZinioSourceApplicationId { get; set; }
        public int ZinioSourceProjectId { get; set; }
        public int ZinioProductType { get; set; }
        public string ZinioPublicationIds { get; set; }

        public int TakeCountForTempUpload { get; set; }

        public double WaitTimeInMinutesForTempUpload { get; set; }
        public string TempLeadFileUploadBlobName { get; set; }
        public string TempLeadFileUploadQueueName { get; set; }
        public string LeadUploadDelimeter { get; set; }
        public double WaitTimeInMinutesForLeadBlobService { get; set; }
        public double WaitTimeInMinutesForLeadFTPService { get; set; }
        public string SalePendingQueueName { get; set; }

        // AlwaysOn Third Party
        public string AlwaysOnApiKey { get; set; }
        public string AlwaysOnBaseUrl { get; set; }
        public string AlwaysOn_Method_CreateVoucherCustomUserName { get; set; }
        public string AlwaysOn_Method_CreateCustomer { get; set; }
        public string AlwaysOn_Method_CancelVoucher { get; set; }
        public string AlwaysOn_Method_GetLocations { get; set; }
        public string AlwaysOn_Method_CheckBalance { get; set; }
        public string AlwaysOn_Method_ChangePassword { get; set; }
        public string AlwaysOn_Param_ApiKey { get; set; }
        public string AlwaysOn_Param_UserName { get; set; }
        public string AlwaysOn_Param_Name { get; set; }
        public string AlwaysOn_Param_SurName { get; set; }
        public string AlwaysOn_Param_Password { get; set; }
        public string AlwaysOn_Param_Mobile { get; set; }
        public string AlwaysOn_Param_VoucherCode { get; set; }
        public string AlwaysOn_Param_Email { get; set; }
        public string AlwaysOn_Param_CellPhone { get; set; }
        public string AlwaysOn_Param_PackageId { get; set; }
        public string AlwaysOn_Param_SingleCode { get; set; }
        public string AlwaysOn_Param_Lata { get; set; }
        public string AlwaysOn_Param_Latb { get; set; }
        public string AlwaysOn_Param_Lnga { get; set; }
        public string AlwaysOn_Param_Lngb { get; set; }


        // ClickaTell Config
        public string ClickatellBaseUrl { get; set; }
        public string ClickatellApiUsername { get; set; }
        public string ClickatellApiPassword { get; set; }
        public string ClickatellApiId { get; set; }
        public bool ClickatellIsTesting { get; set; }


        public int CleansingComit { get; set; }
        public int CleansingVoice { get; set; }
        public int CleansingComitDays { get; set; }
        public int CleansingOtherDays { get; set; }
        public int CleansingTwoWeekPeriod { get; set; }
        public int CleansingCoolOffDate { get; set; }
        public int CleansingSalesPeriod { get; set; }
        public int CleansingRecheckPeriod { get; set; }
        public int RecycledLeadStatusDetailId { get; set; }
        public int RecycledLeadTypeId { get; set; }


        //Debicheck 
        public string DebiCheckAuthUrl { get; set; }
        public string DebiCheckMandateUrl { get; set; }
        public string DebiCheckListMandateUrl { get; set; }
        public string DebiCheckAPIKey { get; set; }
        public string DebiCheckUsername { get; set; }
        public string DebiCheckPassword { get; set; }
        public int DebiCheckMaxRetry { get; set; }
        public string DebicheckMandateStatus { get; set; }
        public string AthenticationInstrument { get; set; }
        public string AuthenticationType { get; set; }
        public string CreditorAccountNumber { get; set; }
        public string CreditorPhoneNumber { get; set; }
        public string CreditorEmail { get; set; }
        public string Shortname { get; set; }
        public string CollectionType { get; set; }
        public string DebitValueType { get; set; }
        public string EntryClassCode { get; set; }
        public string TrackingIndicator { get; set; }
        public string DateAdjustmentAllowed { get; set; }
        public string CurrencyCode { get; set; }
        public string DebicheckQueueName { get; set; }
        public string DebicheckCertLocation { get; set; }
        public string DebicheckCertKey { get; set; }
        public int DebicheckSessionExpireInMinutes { get; set; }
        public int DebicheckMaxDaysToProcessOrders { get; set; }

        public string TinyBaseURL { get; set; }
        public string BulkStatusUploadBlob { get; set; }
        public string LeadCleansingUploadBlob { get; set; }
        //AVSR
        public string AvsrCertLocation { get; set; }
        public string AvsrCertPassword { get; set; }
        public string AvsrUser { get; set; }

        //TCH
        public string TchApiUsername { get; set; }
        public string TchApiPassword { get; set; }
        public string TchCreateFunction { get; set; }
        public string TchCancelFunction { get; set; }
        public string TchActivateFunction { get; set; }
        public bool ThirdPartyServiceIsTesting { get; set; }

        // Driven
        public string DrivenPartnerCode { get; set; }
        public string DrivenPassword { get; set; }
        public string DrivenWebServiceType { get; set; }
        public string DrivenOperation { get; set; }

        public int MinPasswordLength { get; set; }

        #endregion

        #region Utils

        private T GetSetting<T>(string key, T defaultValue = default(T))
        {
            var settingValue = _config.AppSettings.Settings[key]?.Value;
            if (settingValue == null) return defaultValue;
            return (T)Convert.ChangeType(settingValue, typeof(T));
        }
        private string GetConectionString(string key)
        {
            var settingValue = _config.ConnectionStrings.ConnectionStrings[key];
            return settingValue == null ? string.Empty : settingValue.ConnectionString;
        }

        private string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path + uri.Fragment);
                return path;
            }
        }



        public XmlLoggingConfiguration GetLoggingConfiguration()
        {
            var appConfig = ConfigurationManager.OpenExeConfiguration(AssemblyDirectory);

            return new XmlLoggingConfiguration(appConfig.FilePath);
        }

        #endregion
    }

}

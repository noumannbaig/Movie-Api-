using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Application.Enums
{
    public sealed class GeneralMessage
    {
        private GeneralMessage() { }
        public const string StatusFail = "Fail";
        public const string StatusSuccess = "Success";
        public const string StatusWarning = "Warning";
        public const string StatusInfo = "Info";

        public const string ChangesSaved = "Changes have been saved successfully";
        public const string RecordAdded = "Record has been added successfully";
        public const string RecordNotAdded = "Record Not added successfully";
        public const string RecordUpdated = "Record has been updated successfully";
        public const string RecordDeleted = "Record has been deleted successfully";
        public const string DocumentUploaded = "Document has been uploaded successfully";
        public const string DocumentDeleted = "Document has been deleted successfully";
        public const string MarkedCompleted = "Record has been marked as compeleted";
        public const string GetRecord = "Record has been Fetched Successfully";
        public const string SMSSent = "SMS has been sent successfully";
        public const string EmailSent = "Email has been sent successfully";
        public const string EmailSentFail = "Email not sent successfully";
        public const string NickNameAlreadyTaken = "NicknameAlreadyTakenIdentifier";

        public const string RecordNotFound = "Record not found";
        public const string FileNotFound = "File not found";
        public const string RecordNotMatched = "This record does not match. please try another one";


        public const string ServerError = "Server error occured";
        public const string PermissionError = "You are not authorized. Please contact administrator";
        public const string NotSuperAdminError = "Only SuperAdmin is authorized to perform this action";
        public const string DeleteSuperAdminError = "SuperAdmin can't be deleted";
        public const string FileNotUploadedError = "Please upload a file";
        public const string EmailSendError = "Email has not been sent";
        public const string FileNotUploaded = "not_uploaded";
        public const string USERLOGINSUCCESS = "login_successfull";
        public const string ONBOARDINGSUCCESS = "Onboarding Succesfully";
        public const string REFRESHTOKEN = "token_updated";
        public const string LOGOUTSUCCESS = "Logout Successfully";
        public const string GETSETTINGS = "Settings Fetched Successfully";
        public const string UPDATESETTINGS = "Settings Updated Successfully";
        public const string GETNEWS = "News Fetched Successfully";
        public const string BUSINESSPROFILEREQUESTSUCCESS = "business_profile_request_success_message";
        public const string BUSINESSPROFILEREQUESTUPDATE = "business_profile_update_success_message";
        public const string REWARDUPDATESUCCESS = "Reward Updated Successfully";
        public const string UPDATEPROFILE = "update_profile";
        public const string INVALIDCREDENTIALS = "invalid_credential_message";
        public const string AssigneUserRole = "Assigne Role To User";
        public const string PasswordVerifiedMessage = "Password Verified!";
        public const string CodeVerifiedMessage = "Code Verified!";
        public const string UserLoginSuccessMessage = "User login successfull";
        public const string UserProfileCreatedSuccessMessage = "Your profile has been created successfully! Please Login to your account.";
        public const string UserProfileFetchSuccessMessage = "User profile has been Fetched successfully!";
        public const string UserPasswordChangedSuccessMessage = "Password changed successfully!";
        public const string PasswordRecoveryLinkMessage = "PasswordRecoveryLinkIdentifier";
        public const string UserProfileUpdatedSuccessMessage = "Your profile has been updated successfully!";
        public const string DataSucess = "Data fetch successfully!";
        public const string UserLogoutSuccessMessage = "Logged Out Successfully.";
        public const string UnsuccessfulMessage = "Unsuccessful";
        public const string InvalidEmailOrPassword = "Incorrect Email or Password please check!";
        public const string InvalidEmail = "Invalid Email ";
        public const string SendToAndFromDate = "To And From Date Should Be Valid";
    }
    
}

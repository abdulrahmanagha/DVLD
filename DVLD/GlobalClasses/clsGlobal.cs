using DVLD_Business;
using DVLD_Business.Users;
using DVLD_Shared.Utility;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DVLD.GlobalClasses
{
    public static class clsGlobal
    {
        private const string subKeyPath = @"SOFTWARE\DVLD";
        private const string keyPath = @"HKEY_CURRENT_USER\" + subKeyPath;
        private static readonly string valueUsername = "Username";
        private static readonly string valuePassword = "Password";

        public static clsUser CurrentUser { get; set; } = null;

        public static bool RememberUsernameAndPassword(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                _ClearStoredCredentials();
                return false;
            }

            try
            {
                Registry.SetValue(keyPath, valueUsername, username.Trim(), RegistryValueKind.String);
                Registry.SetValue(keyPath, valuePassword, password.Trim(), RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"RegistryPath: {keyPath}, Username: {username?.Trim()}"
                );
                return false;
            }
        }

        public static bool GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath))
                {
                    if (key != null)
                    {
                        string storedUser = key.GetValue(valueUsername, null) as string;
                        string storedPass = key.GetValue(valuePassword, null) as string;

                        if (!string.IsNullOrEmpty(storedUser) && !string.IsNullOrEmpty(storedPass))
                        {
                            username = storedUser;
                            password = storedPass;
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"SubKeyPath: {subKeyPath}");
            }

            return false;
        }

        public static bool HasSavedCredentials()
        {
            string username = null;
            string password = null;
            return GetStoredCredential(ref username, ref password);
        }

        private static void _ClearStoredCredentials()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(subKeyPath, true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(valueUsername, false);
                        key.DeleteValue(valuePassword, false);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"SubKeyPath: {subKeyPath}");
            }
        }

        public static void SyncIfCurrentUser(int updatedUserId)
        {
            if (CurrentUser != null && CurrentUser.UserId == updatedUserId)
            {
                CurrentUser = clsUser.Find(updatedUserId);

                if (CurrentUser != null && HasSavedCredentials())
                {
                    RememberUsernameAndPassword(CurrentUser.UserName, CurrentUser.Password);
                }
            }
        }

        public static void SyncIfCurrentPerson(int updatedPersonId)
        {
            if (CurrentUser != null && CurrentUser.PersonId == updatedPersonId)
            {
                CurrentUser.PersonInfo = clsPerson.Find(updatedPersonId);
            }
        }

        public static bool LoadImage(string path, PictureBox pictureBox)
        {
            try
            {
                DisposeCurrentImage(pictureBox);

                using (Image temp = Image.FromFile(path))
                {
                    pictureBox.Image = new Bitmap(temp);
                }

                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"ImagePath: {path}");
                return false;
            }
        }

        public static void LoadImage(Image image, PictureBox pictureBox)
        {
            DisposeCurrentImage(pictureBox);
            pictureBox.Image = new Bitmap(image);
        }

        public static void DisposeCurrentImage(PictureBox pictureBox)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Dispose();
                pictureBox.Image = null;
            }
        }

    }
}
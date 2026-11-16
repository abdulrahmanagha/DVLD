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
        private const string _subKeyPath = @"SOFTWARE\DVLD";
        private const string _keyPath = @"HKEY_CURRENT_USER\" + _subKeyPath;
        private static readonly string _valueUsername = "Username";
        private static readonly string _valuePassword = "Password";

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
                Registry.SetValue(_keyPath, _valueUsername, username.Trim(), RegistryValueKind.String);
                Registry.SetValue(_keyPath, _valuePassword, password.Trim(), RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"RegistryPath: {_keyPath}, Username: {username?.Trim()}"
                );
                return false;
            }
        }

        public static bool GetStoredCredential(ref string username, ref string password)
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(_subKeyPath))
                {
                    if (key != null)
                    {
                        string storedUser = key.GetValue(_valueUsername, null) as string;
                        string storedPass = key.GetValue(_valuePassword, null) as string;

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
                clsEventLogger.LogEvent(ex, $"SubKeyPath: {_subKeyPath}");
            }

            return false;
        }

        public static bool HasSavedCredentials()
        {
            string username = null;
            string password = null;
            return GetStoredCredential(ref username, ref password);
        }

        public static bool UpdateRememberedUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _ClearStoredCredentials();
                return false;
            }

            try
            {
                Registry.SetValue(_keyPath, _valueUsername, username.Trim(), RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(
                    ex,
                    $"RegistryPath: {_keyPath}, Username: {username?.Trim()}"
                );

                return false;
            }
        }

        private static void _ClearStoredCredentials()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(_subKeyPath, true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(_valueUsername, false);
                        key.DeleteValue(_valuePassword, false);
                    }
                }
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, $"SubKeyPath: {_subKeyPath}");
            }
        }

        public static void SyncCurrentUser(int updatedUserId, string newPlainPassword = null)
        {
            if (CurrentUser == null || CurrentUser.UserId != updatedUserId)
                return;

            CurrentUser = clsUser.Find(updatedUserId);

            if (CurrentUser == null)
                return;

            if (!HasSavedCredentials())
                return;

            if (!CurrentUser.IsActive)
            {
                _ClearStoredCredentials();
                return;
            }

            if (!string.IsNullOrEmpty(newPlainPassword))
            {
                RememberUsernameAndPassword(CurrentUser.UserName, newPlainPassword);
            }
            else
            {
                UpdateRememberedUsername(CurrentUser.UserName);
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
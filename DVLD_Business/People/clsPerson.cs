using DVLD.People;
using DVLD_DataAccess;
using DVLD_Shared;
using DVLD_Shared.Utility;
using System;
using System.Data;


namespace DVLD_Business
{
    public class clsPerson
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;  
                int age = today.Year - DateOfBirth.Year;

                if (today < DateOfBirth.AddYears(age))
                {
                    age--;
                }

                return (byte)age;
            }
        }

        public int PersonId { get; set; }
        public string NationalNo {  get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gender { get; set; }
        public string Address {  get; set; }
        public string Phone {  get; set; }
        public string Email {  get; set; }
        public string ImagePath {  get; set; }
        public int NationalityCountryId { get; set; }
        public string CountryName { get; set; }
        private enMode _mode { get; set; } = enMode.Update;
        private string _originalImagePath = null;

        private clsPerson(int personId, string nationalNo, string firstName, string secondName, string thirdName, string lastName
            , DateTime dateOfBirth, byte gender, string address, string phone
            , string email, int nationalityCountryId, string imagePath, string countryName)
        {
            this.PersonId = personId;
            this.NationalNo = nationalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gender = gender;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.ImagePath = imagePath;
            this._originalImagePath = imagePath;
            this.NationalityCountryId = nationalityCountryId;
            this.CountryName = countryName;
            this._mode = enMode.Update;
        }

        private clsPerson(PersonDTO person)
        {
            this.PersonId = person.PersonId;
            this.NationalNo = person.NationalNo;
            this.FirstName = person.FirstName;
            this.SecondName = person.SecondName;
            this.ThirdName = person.ThirdName;
            this.LastName = person.LastName;
            this.DateOfBirth = person.DateOfBirth;
            this.Gender = person.Gender;
            this.Address = person.Address;
            this.Phone = person.Phone;
            this.Email = person.Email;
            this.ImagePath = person.ImagePath;
            this._originalImagePath = person.ImagePath;
            this.NationalityCountryId = person.NationalityCountryId;
            this.CountryName = person.CountryName;
            this._mode = enMode.Update;
        }

        public clsPerson()
        {
            this.PersonId = -1;
            this.DateOfBirth = DateTime.Now;
            this.Gender = 0;
            this.NationalityCountryId = -1;
            this._mode = enMode.AddNew;
        }

        public string GetName()
        {
            string name = FirstName + " " + SecondName + " ";

            if (!string.IsNullOrWhiteSpace(ThirdName))
                name += ThirdName + " ";

            name += LastName;

            return name;
        }

        public static clsPerson Find(int personId)
        {
            PersonDTO personDTO = new PersonDTO
            {
                PersonId = personId
            };

            if(clsPersonData.GetPersonInfoById(personDTO))
            {
                return new clsPerson(personDTO);
            }
            else
            {
                return null;
            }
        }

        public static clsPerson Find(string nationalNo)
        {
            PersonDTO person = new PersonDTO()
            {
                NationalNo = nationalNo
            };
            
            if (clsPersonData.GetPersonInfoByNationalNo(person))
            {
                return new clsPerson(person);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewPerson()
        {
            if (!HandleImageForAddNew())
                return false;

            PersonDTO personDTO = new PersonDTO
            {
                PersonId = this.PersonId,
                NationalNo = this.NationalNo,
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                ThirdName = this.ThirdName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender,
                Address = this.Address,
                Phone = this.Phone,
                Email = this.Email,
                NationalityCountryId = this.NationalityCountryId,
                ImagePath = this.ImagePath
            };

            this.PersonId = clsPersonData.AddNewPerson(personDTO);

            _originalImagePath = ImagePath;

            return PersonId != -1;
        }

        private bool _UpdatePerson()
        {
            if (!HandleImageForUpdate())
                return false;

            PersonDTO personDTO = new PersonDTO
            {
                PersonId = this.PersonId,
                NationalNo = this.NationalNo,
                FirstName = this.FirstName,
                SecondName = this.SecondName,
                ThirdName = this.ThirdName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Gender = this.Gender,
                Address = this.Address,
                Phone = this.Phone,
                Email = this.Email,
                NationalityCountryId = this.NationalityCountryId,
                ImagePath = this.ImagePath              
            };

            bool updated = clsPersonData.UpdatePerson(personDTO);

            if (updated)
                _originalImagePath = ImagePath;

            return updated;
        }

        public bool Save()
        {
            switch(_mode)
            {
                case enMode.AddNew:
                    {
                        if(_AddNewPerson())
                        {
                            this._mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                case enMode.Update:
                    {
                        return _UpdatePerson();
                    }
            }

            return false;
        }

        private static enDeleteResult DeletePerson(clsPerson person)
        {
            if (person == null)
                return enDeleteResult.NotFound;

            enDeleteResult dataAccessResult = clsPersonData.DeletePerson(person.PersonId);

            if ( (dataAccessResult == enDeleteResult.Success) && (!string.IsNullOrWhiteSpace(person.ImagePath)))
                clsFileHelper.DeleteFile(person.ImagePath);

            return dataAccessResult;
        }

        public static enDeleteResult Delete(int personId)
        {
            return DeletePerson(clsPerson.Find(personId));
        }

        public static enDeleteResult Delete(string nationalNo)
        {
            return DeletePerson(clsPerson.Find(nationalNo));
        }

        public static bool IsExistPerson(int personId)
        {
            return clsPersonData.IsExistPerson(personId);
        }

        public static bool IsExistPerson(string nationalNo)
        {
            return clsPersonData.IsExistPerson(nationalNo);
        }
    
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
    
        private bool AddNewImageToFolder()
        {
            string newPath = string.Empty;
            bool isSuccess = false;

            try
            {
                isSuccess = clsFileHelper.CopyFileToFolder(
                    ImagePath,
                    clsSettings.PeopleImagesFolderPath,
                    out newPath
                );
            }
            catch (Exception ex)
            {
                clsEventLogger.LogEvent(ex, "clsPerson.AddNewImageToFolder");
            }


            if (!isSuccess)
                return false;

            this.ImagePath = newPath;
            return true;
        }

        private bool HandleImageForAddNew()
        {
            if (string.IsNullOrWhiteSpace(ImagePath))
                return true;
            return AddNewImageToFolder();
        }

        private bool HandleImageForUpdate()
        {
            if(_originalImagePath == ImagePath)
                return true;

            if (string.IsNullOrWhiteSpace(_originalImagePath))
                return AddNewImageToFolder();

            if (string.IsNullOrWhiteSpace(ImagePath))
                return clsFileHelper.DeleteFile(_originalImagePath);

            bool isDeleted = clsFileHelper.DeleteFile(_originalImagePath);

            if (!isDeleted)
                return false;

            bool addedImage = AddNewImageToFolder();

            if (!addedImage)
                return false;

            return true;
        }
    }
}

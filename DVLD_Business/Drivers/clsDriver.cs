using DVLD_Shared;
using System;
using System.Data;

namespace DVLD_Business.Drivers
{
    public class clsDriver
    {
        private enum enMode { Update = 0, AddNew = 1 };

        public int DriverId { get; set; }
        public int PersonId { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; }

        private enMode _mode = enMode.Update;

        public clsPerson PersonInfo = null;

        public clsDriver()
        {
            DriverId = -1;
            _mode = enMode.AddNew;
        }

        private clsDriver(DriverDTO driverDTO, clsPerson person)
        {
            DriverId = driverDTO.DriverId;
            PersonId = driverDTO.PersonId;
            CreatedByUserId = driverDTO.CreatedByUserId;
            CreatedDate = driverDTO.CreatedDate;

            if(person != null)
            {
                PersonInfo = person;
            }

            _mode = enMode.Update;
        }

        public static clsDriver Find(int driverId)
        {

            DriverDTO driverDTO = new DriverDTO
            {
                DriverId = driverId
            };

            bool isFoud = clsDriverData.GetDriverInfoByDriverId(driverDTO);

            if (isFoud)
            {
                clsPerson person = clsPerson.Find(driverDTO.PersonId);
                return new clsDriver(driverDTO, person);
            }
            else
            {
                return null;
            }
        }

        public static clsDriver FindByPersonId(int personId)
        {
            DriverDTO driverDTO = new DriverDTO
            {
                PersonId = personId
            };

            bool isFound = clsDriverData.GetDriverInfoByPersonId(driverDTO);

            if (isFound)
            {
                clsPerson person = clsPerson.Find(driverDTO.PersonId);
                return new clsDriver(driverDTO, person);
            }
            else
                return null;
        }

        private bool _AddNewDriver()
        {
            DriverDTO driverDTO = new DriverDTO
            {
                PersonId = this.PersonId,
                CreatedByUserId = this.CreatedByUserId,
            };

            DriverId = clsDriverData.AddNewDriver(driverDTO);
            return DriverId != -1;
        }

        private bool _UpdateDriver()
        {
            DriverDTO driverDTO = new DriverDTO
            {
                DriverId = this.DriverId,
                PersonId = this.PersonId,
                CreatedByUserId = this.CreatedByUserId,
                CreatedDate = this.CreatedDate
            };

            return clsDriverData.UpdateDriver(driverDTO);
        }

        public bool Save()
        {
            switch (_mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewDriver())
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
                        return _UpdateDriver();
                    }
            }

            return false;
        }

        public static enDeleteResult Delete(int driverId)
        {
            return clsDriverData.DeleteDriver(driverId);
        }

        public static bool IsDriverExist(int driverId)
        {
            return clsDriverData.IsDriverExist(driverId);
        }

        public static bool IsDriverExistForPersonId(int personId)
        {
            return clsDriverData.IsDriverExistForPersonId(personId);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }

        public static DataTable GetAllDriversWithDetails()
        {
            return clsDriverData.GetAllDriversWithDetails();
        }

    }
}

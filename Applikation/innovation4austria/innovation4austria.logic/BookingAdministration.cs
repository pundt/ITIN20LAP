using innovation4austria.data;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class BookingAdministration
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static List<Booking> GetBookings(int idUser, DateTime? start, DateTime? end)
        {
            log.Info("GetBookings(idUser, start, end)");
            List<Booking> allBookings = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    User user = context.AllUsers.Where(x => x.ID == idUser).FirstOrDefault();
                    if (user==null)
                    {
                        throw new Exception($"Invalid idUser {idUser}");
                    }

                    var result = context.AllBookings
                        .Include("AllBookingDetails")
                        .Include("Room")
                        .Include("Room.Type")
                        .Include("Company")
                        .Include("Room.Building")
                        .Include("Room.AllRoomFacilities")
                        .Include("Room.AllRoomFacilities.Facility")
                        .Where(x => x.Active && x.Company == user.Company);

                    if (start.HasValue)
                    {
                        result = result.Where(x => x.AllBookingDetails.Any(y => y.Date >= start.Value));
                    }

                    if (end.HasValue)
                    {
                        result = result.Where(x => x.AllBookingDetails.Any(y => y.Date <= end.Value));
                    }

                    allBookings = result.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetBookings", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetBookings (inner)", ex.InnerException);
                    throw;
                }
            }

            return allBookings;
        }
        public static List<Booking> GetCompanyBookings(int idCompany, DateTime? start, DateTime? end)
        {
            log.Info("GetCompanyBookings(idCompany, start, end)");
            List<Booking> allBookings = null;

            using (var context = new innovation4austriaEntities())
            {
                try
                {
                    var result = context.AllBookings
                        .Include("AllBookingDetails")
                        .Include("Room")
                        .Include("Room.Type")
                        .Include("Company")
                        .Include("Room.Building")
                        .Include("Room.AllRoomFacilities")
                        .Include("Room.AllRoomFacilities.Facility")
                        .Where(x => x.Active && x.ID_Company == idCompany);

                    if (start.HasValue)
                    {
                        result = result.Where(x => x.AllBookingDetails.Any(y => y.Date >= start.Value));
                    }

                    if (end.HasValue)
                    {
                        result = result.Where(x => x.AllBookingDetails.Any(y => y.Date <= end.Value));
                    }

                    allBookings = result.ToList();
                }
                catch (Exception ex)
                {
                    log.Error("Exception in GetCompanyBookings", ex);
                    if (ex.InnerException != null)
                        log.Error("Exception in GetCompanyBookings (inner)", ex.InnerException);
                    throw;
                }
            }

            return allBookings;
        }

        public static bool BookRoom(int idUser, int idRoom, int? idCompany, DateTime start, DateTime end)
        {
            log.Info("BookRomm(idUser, idRoom, idCompany, start, end)");
            bool bookResult = false;

            try
            {
                using (var context = new innovation4austriaEntities())
                {
                    /// create new booking and booking details
                    User user = context.AllUsers.Where(x => x.ID == idUser).FirstOrDefault();
                    if (user == null)
                    {
                        throw new Exception($"Invalid idUser {idUser}");
                    }

                    Company company = context.AllCompanies.Where(x => x.ID == idCompany).FirstOrDefault();
                    if (company==null)
                    {
                        throw new Exception($"Invalid idCompany {idCompany}");
                    }

                    Room room = context.AllRooms.Where(x => x.ID == idRoom).FirstOrDefault();
                    if (room == null)
                    {
                        throw new Exception($"Invalid idRoom {idRoom}");
                    }

                    Booking newBooking = new Booking()
                    {
                        // either given idCompany or companyId of given User
                        ID_Company = idCompany.HasValue ? idCompany.Value : user.Company.ID,
                        ID_Room = idRoom,
                        Active = true                        
                    };

                    DateTime current = start;
                    while (current<=end)
                    {
                        BookingDetail newDetail = new BookingDetail()
                        {
                            Date = current,
                            Price = room.PricePerDay
                        };
                        newBooking.AllBookingDetails.Add(newDetail);

                        current = current.AddDays(1);
                    }

                    context.AllBookings.Add(newBooking);
                    context.SaveChanges();
                    log.Info($"User {idUser} booked room {idRoom} for company {idCompany}");
                }
                bookResult = true;
            }
            catch (Exception ex)
            {
                log.Error("Exception in BookRoom", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in BookRoom (inner)", ex.InnerException);
                throw;
            }

            return bookResult;
        }

        public static bool CancelBooking(int idUser, int idBooking)
        {
            log.Info("CancelBooking(idUser, idBooking");
            bool canceled = false;

            try
            {
                using (var context =new innovation4austriaEntities())
                {
                    User user = context.AllUsers.Where(x => x.ID == idUser).FirstOrDefault();
                    if (user == null)
                    {
                        throw new Exception($"Invalid idUser {idUser}");
                    }

                    Booking booking= context.AllBookings
                        // only users from company that created booking may cancel them
                        .Where(x => x.ID == idBooking && x.ID_Company == user.Company.ID)
                        .FirstOrDefault();

                    if (booking == null)
                    {
                        throw new Exception($"Invalid idBooking {idBooking}");
                    }

                    booking.Active = false;
                    context.SaveChanges();
                }
                canceled = true;
            }
            catch (Exception ex)
            {
                log.Error("Exception in CancelBooking", ex);
                if (ex.InnerException != null)
                    log.Error("Exception in CancelBooking (inner)", ex.InnerException);
                throw;
            }

            return canceled;
        }
    }
}

using AutoMapper;
using BAL.ModelView;
using BAL.Service.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingReservationController : ControllerBase
    {
        private readonly IBookingReservationService bookingReservationService;
        private readonly IBookingDetailService bookingDetailService;
        private readonly IRoomInformationService roomInformationService;
        private readonly ICustomerService customerService;
        private readonly IMapper mapper;
        public BookingReservationController(IBookingReservationService bookingReservationService, IBookingDetailService bookingDetailService,
            IRoomInformationService roomInformationService, ICustomerService customerService, IMapper mapper)
        {
            this.mapper = mapper;
            this.bookingReservationService = bookingReservationService;
            this.bookingDetailService = bookingDetailService;
            this.roomInformationService = roomInformationService;
            this.customerService = customerService;
        }

        [HttpGet("/api/v1/BookingReservation")]
        public IActionResult GetAll()
        {
            var book = bookingReservationService.GetAll().OrderByDescending(r => r.BookingReservationId)
                .ToList();
            return Ok(book);
        }
        [HttpGet("/api/v1/BookingReservation/{startDate}&&{endDate}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingReservation>))]
        [ProducesResponseType(400)]
        public IActionResult GetReport(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                return Ok("StratDate must be less than EndDate");
            }
            if (startDate == null && endDate == null)
            {
                return Ok("Please select StartDate and EndDate");
            }

            var bookingReservations = bookingReservationService.GetAll().Where(br => br.BookingDate >= startDate && br.BookingDate <= endDate).Select(br => new { br.BookingDate, br.BookingReservationId, br.TotalPrice, br.BookingDetails }).OrderByDescending(r => r.BookingReservationId).ToList();
            return Ok(bookingReservations);
        }

        [HttpGet("/api/v1/BookingReservation/{customerId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookingReservation>))]
        [ProducesResponseType(400)]
        public IActionResult GetByCustomerId(int customerId)
        {
            try
            {
                var reservationHistoryList = from reservation in bookingReservationService.GetAll()
                                             where reservation.CustomerId == customerId
                                             join detail in bookingDetailService.GetAll() on reservation.BookingReservationId equals detail.BookingReservationId
                                             join roomInfo in roomInformationService.GetAllRoom() on detail.RoomId equals roomInfo.RoomId
                                             join cust in customerService.GetAll() on reservation.CustomerId equals cust.CustomerId
                                             orderby reservation.BookingReservationId descending
                                             select new BookingReservationView
                                             {
                                                 RoomNumber = roomInfo.RoomNumber,
                                                 StartDate = detail.StartDate,
                                                 EndDate = detail.EndDate,
                                                 ActualPrice = (decimal)detail.ActualPrice,
                                                 CustomerId = reservation.CustomerId,
                                                 CustomerFullName = cust.CustomerFullName
                                             };
                return Ok(reservationHistoryList.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

﻿var monthNamesRy = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var daysOfTheWeekRy = ["S", "M", "T", "W", "T", "F", "S"]
var selectedDate = new Date();
$(document).ready(() => {
	var d = new Date();
	var year = d.getFullYear();
	$("#year").text(year);
	var thisMonth = d.getMonth();// 0 - 11
	var today = d.getDate();// 1 -31
	//var nthday = d.getDay();// 0 - 7
	var daysOfTheMonthDiv = $(".daysOfTheMonth");


	function createCalendar(month) {
		var monthDiv = createMonthHeader(month);

		var firstDayOfTheMonth = getFirstDayOfTheMonth(year, month);
		var daysinmonth = daysInMonth(year, month)
		var counter = 0, order = 6;

		for (var i = 0; i < firstDayOfTheMonth + 7; i++) {
			order++;
			createDay(month, "&nbsp;", order, monthDiv);
		}
		for (var i = firstDayOfTheMonth; i < daysInMonth(year, month) + firstDayOfTheMonth; i++) {
			counter++;
			order++;
			createDay(month, counter, order, monthDiv);
		}

		for (var i = firstDayOfTheMonth + daysinmonth; i < 6 * 7; i++) {
			order++;
			createDay(month, "&nbsp;", order, monthDiv);
		}
	}

	createCalendar(thisMonth);

	function createDay(month, counter, order, monthDiv) {
		var day = $('<div>');
		if (month === thisMonth && counter === today) {
			day.addClass('to day');
		} else if (counter < today || counter > today + 7) {
			day.addClass('day disabled');
		} else {
			day.addClass('day');
		}

		day.css('order', order);

		if (counter === "&nbsp;" || counter === "") {
			day.text("");
			day.addClass('disabled');
		} else {
			day.text(counter);
		}
		monthDiv.append(day);
	}


	function createMonthHeader(month) {
		var calendar = $(".calendar");


		var monthDiv = $("<div>");
		monthDiv.addClass("month");
		calendar.append(monthDiv);

		var h4 = $("<h4>");
		h4.text(monthNamesRy[month]);
		monthDiv.append(h4);

		for (var i = 0; i < 7; i++) {
			//var order = (i == 0) ? order = 7 : order = i;
			var hday = $("<div>");
			hday.addClass("day OfWeek");
			hday.css("order", i);
			hday.text(daysOfTheWeekRy[i].toUpperCase());
			monthDiv.append(hday);
		}


		return monthDiv;
	}


	function daysInMonth(year, month) {
		return new Date(year, month + 1, 0).getDate();
	}

	function getMonthName(month) {
		return monthNamesRy[month];
	}
	function getDayName(day) {
		return daysOfTheWeekRy[day];
	}

	function getFirstDayOfTheMonth(y, m) {
		var firstDay = new Date(y, m, 1);
		return firstDay.getDay();
	}
	function getLastDayOfTheMonth(y, m) {
		var lastDay = new Date(y, m + 1, 0);
		return lastDay.getDay();
	}

	$(".day").on("click", function () {
		$("#book-court-btn").prop("disabled", true);

		// get selected date
		var selectedItem = $(".to");

		// remove selected
		selectedItem.removeClass("to");

		// set selected to clicked item
		$(this).removeClass("day").addClass("to day")

		// update selected date
		selectedDate = new Date(year, thisMonth, $(this).text());

		// remove selected court slot
		selectedCourtSlot = [];

		// reset price
		price = +0;
		$('#total-price').text(price + " VND");

		var value = $("#sltCourt").val();
		let y = selectedDate.getFullYear();
		let m = String(selectedDate.getMonth() + 1).padStart(2, '0');
		let d = String(selectedDate.getDate()).padStart(2, '0');
		let formattedDate = `${y}-${m}-${d}`;

		$.ajax({
			method: 'GET',
			url: "/customer/bookings/byday/?handler=UpdateSlotTime",
			data: {
				courtId: value,
				date: formattedDate
			},
			contentType: 'application/json',
			success: function (response) {
				if (response.isSuccess && response.data) {
					updateSlotTime(response.data);
					selectSlotTime(value);
				}

				// Xử lý response
			},
			error: function (xhr, status, error) {
				console.log("AJAX Error:");
				console.log("Status:", status);
				console.log("Error:", error);
				console.log("Response Text:", xhr.responseText);
			}
		})
	});
})

const resetCalendar = () => {
	var d = new Date();
	var today = d.getDate();

	// get selected date
	var selectedItem = $(".to");

	// remove selected
	selectedItem.removeClass("to");

	var days = $(".day");
	days.each(function () {
		if (+$(this).text() === today) {
			$(this).removeClass("day").addClass("to day");
		}
		$(this).addClass('disabled');
	});
}


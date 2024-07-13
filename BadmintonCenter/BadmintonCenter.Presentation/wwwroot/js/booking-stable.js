var price = +0;
var selectedSlots = [];
var selectedCourts = [];
var selectedDayOfWeek = [];
var bookingBtn = $('#book-court-btn');

$(document).ready(function () {
    var userId = $("#userId").val();
    var currentMonth = $("#month").val();

    // unable all check court
    $(".court .form-check-input").prop("disabled", true);

    // unable booking btn
    bookingBtn.prop("disabled", true);
    bookingBtn.css({
        'background-color': "grey",
        'cursor': "pointer"
    });

    updateEventClickSlot();

    updateEventClickDay();

    updateEventClickCourt();

    bookingBtn.click(function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        var dateObject = new Date(currentMonth);
        var month = dateObject.getMonth() + 1;

        var bookingData = {
            courts: selectedCourts,
            slots: selectedSlots,
            dayOfWeek: selectedDayOfWeek,
            month: month,
            userId: userId
        }

        console.log(bookingData);

        // call ajax to add booking
        $.ajax({
            url: "/customer/booking/stable/?handler=Booking",
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(bookingData),
            success: function (response) {
                console.log("success");
                if (response.isSuccess && response.data) {
                    // set successfull message
                    $('.success-message').text("Booking Successfully");
                    $('.success-message').prop('hidden', false);

                    // delete message
                    setTimeout(function () {
                        $('.success-message').empty()
                        $('.success-message').prop('hidden', true);
                    }, 3000);

                    $('.time-slot').prop("disabled", true);
                    $('.dayOfWeek').prop("disabled", true);
                    $("#payment-btn").prop("hidden", false);
                    $("#payment-btn a").attr("href", "/Customer/Booking/Detail?id=" + response.id);
                    bookingBtn.prop("disabled", true);
                    bookingBtn.css({
                        'background-color': "grey",
                        'cursor': "pointer"
                    });
                }
            },
            error: function (xhr, status, error) {
                console.log("AJAX Error:");
                console.log("Status:", status);
                console.log("Error:", error);
                console.log("Response Text:", xhr.responseText);
            }
        });
    });
})

const updateEventClickSlot = () => {
    // get all time slot items
    var slotItems = $('.time-slot');
    var dateObject = new Date($("#month").val());

    // handle when choosing slot time
    slotItems.on('click', function () {
        // Toggle class 'active' when clicking
        $(this).toggleClass('active');

        // check div tag if it has class 'active'
        if ($(this).hasClass('active')) {
            // change color
            $(this).css('background-color', '#c0c0c0');

            // update selected court slot
            selectedSlots.push(+$(this).attr('id'))

            if (selectedSlots.length > 0 && selectedDayOfWeek.length > 0) {
                $(".court .form-check-input").prop("disabled", false);

                var getItems = {
                    month: dateObject.getMonth() + 1,
                    daysOfWeek: JSON.stringify(selectedDayOfWeek),
                    slotTimes: JSON.stringify(selectedSlots)
                }

                $.ajax({
                    method: 'GET',
                    url: "/customer/booking/stable/?handler=UpdateCourts",
                    data: getItems,
                    contentType: 'application/json',
                    success: function (response) {
                        if (response.isSuccess && response.data) {
                            updateCourts(response.data);

                            updateEventClickCourt();
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log("AJAX Error:");
                        console.log("Status:", status);
                        console.log("Error:", error);
                        console.log("Response Text:", xhr.responseText);
                    }
                })
            }

        } else {
            // change color
            $(this).css('background-color', '#f1f1f1');

            // update selected court slot
            const index = selectedSlots.findIndex(item => item === +$(this).attr('id'));

            if (index !== -1) {
                selectedSlots.splice(index, 1);
            }

            if (selectedDayOfWeek.length > 0) {
                var getItems = {
                    month: dateObject.getMonth() + 1,
                    daysOfWeek: JSON.stringify(selectedDayOfWeek),
                    slotTimes: JSON.stringify(selectedSlots)
                }

                $.ajax({
                    method: 'GET',
                    url: "/customer/booking/stable/?handler=UpdateCourts",
                    data: getItems,
                    contentType: 'application/json',
                    success: function (response) {
                        if (response.isSuccess && response.data) {
                            updateCourts(response.data);

                            updateEventClickCourt();

                            if (selectedSlots.length == 0 || selectedDayOfWeek.length == 0) {
                                $(".court .form-check-input").prop("disabled", true);
                                $(".court .form-check-input").prop("checked", false);
                                selectedCourts = [];
                                bookingBtn.prop("disabled", true);
                                bookingBtn.css({
                                    'background-color': "grey",
                                    'cursor': "pointer"
                                });
                            }
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log("AJAX Error:");
                        console.log("Status:", status);
                        console.log("Error:", error);
                        console.log("Response Text:", xhr.responseText);
                    }
                })
            }

            if (selectedSlots.length == 0 || selectedDayOfWeek.length == 0) {
                $(".court .form-check-input").prop("disabled", true);
                $(".court .form-check-input").prop("checked", false);
                selectedCourts = [];
                bookingBtn.prop("disabled", true);
                bookingBtn.css({
                    'background-color': "grey",
                    'cursor': "pointer"
                });
            }
        }
    })
}

const updateEventClickDay = () => {
    // get all dayOfWeek
    var dayItems = $('.dayOfWeek');
    var dateObject = new Date($("#month").val());

    // handle when choosing dayOfWeek
    dayItems.on('click', function () {
        // check input if it is checked
        if ($(this).prop('checked')) {
            // update selected day of week
            selectedDayOfWeek.push($(this).prop('value'))

            if (selectedSlots.length > 0 && selectedDayOfWeek.length > 0) {
                $(".court .form-check-input").prop("disabled", false);
            }

            selectedSlots = [];
            $(".court .form-check-input").prop("disabled", true);


            var getItems = {
                month: dateObject.getMonth() + 1,
                daysOfWeek: JSON.stringify(selectedDayOfWeek),
            }

            $.ajax({
                method: 'GET',
                url: "/customer/booking/stable/?handler=AvailableTime",
                data: getItems,
                contentType: 'application/json',
                success: function (response) {
                    if (response.isSuccess && response.data) {
                        console.log(response.data);

                        // get container of slot time
                        var dataContainer = $('.container-slot');

                        // clear element in data slot time UI
                        dataContainer.empty();

                        // check data if it not null or empty
                        if (response.data.length > 0) {
                            response.data.forEach((item) => {
                                var jsonItem = JSON.parse(item);
                                dataContainer.append(`<div id=${jsonItem.id} class="time-slot" price=${jsonItem.price}>${jsonItem.startTime} - ${jsonItem.endTime}</div>`)
                            });
                        }

                        updateEventClickSlot();
                    }
                },
                error: function (xhr, status, error) {
                    console.log("AJAX Error:");
                    console.log("Status:", status);
                    console.log("Error:", error);
                    console.log("Response Text:", xhr.responseText);
                }
            })

        } else {
            // update selected day of week
            const index = selectedDayOfWeek.findIndex(item => item === $(this).prop('value'));

            if (index !== -1) {
                selectedDayOfWeek.splice(index, 1);
            }

            $(".court .form-check-input").prop("disabled", true);

            if (selectedSlots.length == 0 || selectedDayOfWeek.length == 0) {
                $(".court .form-check-input").prop("disabled", true);
                $(".court .form-check-input").prop("checked", false);
                selectedCourts = [];
                bookingBtn.prop("disabled", true);
                bookingBtn.css({
                    'background-color': "grey",
                    'cursor': "pointer"
                });
            }

            selectedSlots = [];

            var getItems = {
                month: dateObject.getMonth() + 1,
                daysOfWeek: JSON.stringify(selectedDayOfWeek),
            }

            $.ajax({
                method: 'GET',
                url: "/customer/booking/stable/?handler=AvailableTime",
                data: getItems,
                contentType: 'application/json',
                success: function (response) {
                    if (response.isSuccess && response.data) {
                        // get container of slot time
                        var dataContainer = $('.container-slot');

                        // clear element in data slot time UI
                        dataContainer.empty();

                        // check data if it not null or empty
                        if (response.data.length > 0) {
                            response.data.forEach((item) => {
                                var jsonItem = JSON.parse(item);
                                dataContainer.append(`<div id=${jsonItem.id} class="time-slot" price=${jsonItem.price}>${jsonItem.startTime} - ${jsonItem.endTime}</div>`)
                            });
                        }

                        updateEventClickSlot();
                    }
                },
                error: function (xhr, status, error) {
                    console.log("AJAX Error:");
                    console.log("Status:", status);
                    console.log("Error:", error);
                    console.log("Response Text:", xhr.responseText);
                }
            })
        }
    })
}

const updateEventClickCourt = () => {
    // get all courts
    var courtItems = $(".court .form-check-input");

    // handle when choosing court
    courtItems.on('click', function () {
        // check input if it is checked
        if ($(this).prop('checked')) {
            // update selected court
            selectedCourts.push(+$(this).prop('value'))

            if (selectedCourts.length > 0) {
                $("#book-court-btn").prop("disabled", false);
                bookingBtn.css({
                    'background-color': "#007bff",
                    'cursor': "pointer"
                });
            }

        } else {
            // update selected court
            const index = selectedCourts.findIndex(item => item === +$(this).prop('value'));

            if (index !== -1) {
                selectedCourts.splice(index, 1);
            }

            if (selectedCourts.length == 0) {
                bookingBtn.prop("disabled", true);
                bookingBtn.css({
                    'background-color': "grey",
                    'cursor': "pointer"
                });
            }
        }

        console.log(selectedCourts)
    })
}

const updateCourts = (data) => {
    var container = $(".container-court");

    container.empty();

    if (data == null || data.length == 0) {
        container.append('<p>No available court</p>');
    } else {
        data.forEach((item) => {
            var jsonItem = JSON.parse(item);
            container.append(`<div class="court form-check">
                    <input class="form-check-input" type="checkbox" value="${jsonItem.id}">
                    <label class="form-check-label">
                        ${jsonItem.name}
                    </label>
                </div>`);
        });
    }



}


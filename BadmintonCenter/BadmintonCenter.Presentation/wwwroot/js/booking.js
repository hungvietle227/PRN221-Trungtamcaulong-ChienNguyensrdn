var price = +0;

$(document).ready(function () {

    // handle when changing option
    $("#sltCourt").change(function () {
        // get value of selected option
        var value = $("#sltCourt").val();

        $.ajax({
            method: 'GET',
            url: "/booking/byday/?handler=UpdateSlotTime",
            data: {
                courtId: value,
                date: selectedDate.toLocaleDateString()
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
    })

    // handle when choosing slot time
    updateEventClickItem();

    $('#book-court-btn').click(function () {
        var token = $('input[name="__RequestVerificationToken"]').val();
        var bookingData = {
            details: selectedCourtSlot,
            price: price
        }

        // call ajax to add booking
        $.ajax({
            url: "/booking/byday/?handler=Booking",
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(bookingData),
            success: function (response) {
                if (response.isSuccess && response.data) {
                    updateSlotTime(response.data);
                    resetUI();
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
});
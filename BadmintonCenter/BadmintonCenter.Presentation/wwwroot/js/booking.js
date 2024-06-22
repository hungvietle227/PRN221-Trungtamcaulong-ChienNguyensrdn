var price = +0;

$(document).ready(function () {
    var userId = $("#userId").val();
   
    $('#book-court-btn').prop("disabled", true);

    // handle when changing option
    $("#sltCourt").change(function () {
        // get value of selected option
        var value = $("#sltCourt").val();

        $.ajax({
            method: 'GET',
            url: "/customer/booking/byday/?handler=UpdateSlotTime",
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
        var newDate = new Date(selectedDate);
        let year = newDate.getFullYear();
        let month = String(newDate.getMonth() + 1).padStart(2, '0');
        let day = String(newDate.getDate()).padStart(2, '0');
        let formattedDate = `${year}-${month}-${day}`;

        var bookingData = {
            details: selectedCourtSlot,
            price: parseInt(price),
            validDate: formattedDate,
            userId: userId
        }

        console.log(JSON.stringify(bookingData))

        // call ajax to add booking
        $.ajax({
            url: "/customer/booking/byday/?handler=Booking",
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            headers: {
                'RequestVerificationToken': token
            },
            data: JSON.stringify(bookingData),
            success: function (response) {
                if (response.isSuccess && response.data) {
                    updateSlotTime(response.data);
                    resetUI();
                    $("#payment-btn").prop("hidden", false);
                    $("#payment-btn a").attr("href", "/Customer/Booking/Detail?id=" + response.id);
                    $('#book-court-btn').prop("disabled", true);
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
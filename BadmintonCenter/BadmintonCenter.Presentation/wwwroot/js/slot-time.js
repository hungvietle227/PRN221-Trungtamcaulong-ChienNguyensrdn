
var selectedCourtSlot = [];

const updateSlotTime = (data) => {
    // get container of slot time
    var dataContainer = $('.container-slot');

    // clear element in data slot time UI
    dataContainer.empty();

    // check data if it not null or empty
    if (data.length > 0) {
        data.forEach((item) => {
            var jsonItem = JSON.parse(item);
            dataContainer.append(createSlotItem(jsonItem.id, jsonItem.startTime, jsonItem.endTime, jsonItem.price));
        });
    }

    updateEventClickItem();
}

const createSlotItem = (id, start, end, price) => {
    // create new item
    var item = $('<div>').addClass('time-slot')
        .attr('price', price)
        .attr('id', id)
        .text(start + " - " + end);

    return item;
}

const updateEventClickItem = () => {
    // get all time slot items
    var slotItems = $('.time-slot');

    // get value of selected option
    var courtId = $("#sltCourt").val();

    // handle when choosing slot time
    slotItems.on('click', function () {
        // Toggle class 'active' when clicking
        $(this).toggleClass('active');

        // check div tag if it has class 'active'
        if ($(this).hasClass('active')) {
            // change color
            $(this).css('background-color', '#c0c0c0');

            // update price
            updatePrice('add', $(this).attr('price'))

            // update selected court slot
            selectedCourtSlot.push({
                courtId: +courtId,
                slotTimeId: +$(this).attr('id')
            })

        } else {
            // change color
            $(this).css('background-color', '#f1f1f1');

            // update price
            updatePrice('remove', $(this).attr('price'))

            // update selected court slot
            const index = selectedCourtSlot.findIndex(item => item.courtId === +courtId && item.slotTimeId === +$(this).attr('id'));

            if (index !== -1) {
                selectedCourtSlot.splice(index, 1);
            }
        }
    })
}

const updatePrice = (type, slotPrice) => {
    // get price item in ui
    var priceItem = $('#total-price');

    // update global price
    if (type === 'add') {
        price += +slotPrice;
    } else {
        price -= +slotPrice;
    }

    // set ui
    priceItem.text(price + " VND")
}

const resetUI = () => {
    // update selected option
    $("#sltCourt").val(1);

    // update price
    price = +0;
    $('#total-price').text(price + " VND");

    // update selected court time
    selectedCourtSlot = [];

    // set successfull message
    $('.success-message').text("Đặt sân thành công");
    $('.success-message').prop('hidden', false);

    // delete message
    setTimeout(function () {
        $('.success-message').empty()
        $('.success-message').prop('hidden', true);
    }, 3000);
}

const selectSlotTime = (courtId) => {
    // select court time of court 
    let courtTimeElement = selectedCourtSlot.filter(item => item.courtId === +courtId);

    courtTimeElement.forEach(item => {
        // get element
        var ele = $(`.time-slot[id=${item.slotTimeId}]`)

        // add active class
        ele.addClass("active");

        // change color
        ele.css('background-color', '#c0c0c0');
    });
}
$(() => {

    $("#confirm").on('click', function () {
        const id = $("#confirm").data('id');
        $.post('/home/confirm', { id }, function () {
            removeButtons();
            updateCounts();
        })
    })

    $("#decline").on('click', function () {
        const id = $("#decline").data('id');
        $.post('/home/decline', { id }, function () {
            removeButtons();
            updateCounts();
        })
    })

    const removeButtons = () => {
        $("#confirm").remove();
        $("#decline").remove();
    }

    const updateCounts = () => {
        $.get('/home/getCounts', function (counts) {
            $("#pending").text(counts.pending);
            $("#confirmed").text(counts.confirmed);
            $("#declined").text(counts.declined);
        })
    }
})
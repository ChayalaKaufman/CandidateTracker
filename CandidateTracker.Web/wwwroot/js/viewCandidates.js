$(() => {

    $("#toggle").on('click', function () {
        $(".notes").toggle();
        $("#notesHeader").toggle();
    })
})
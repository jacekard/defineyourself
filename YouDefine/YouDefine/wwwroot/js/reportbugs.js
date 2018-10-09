(function () {
    console.log("reportbugs dzialam");

    $("#bug-submit").click(function () {
        var response = $("#bug-report-text").val();
        if (response === "") {
            return;
        }
        $.ajax({
            type: 'POST',
            url: 'api/bugs',
            data: {
                'information': response
            },
            complete: function (data) {
                console.log(data);
                window.close(this);
            }
        });
    });
})();
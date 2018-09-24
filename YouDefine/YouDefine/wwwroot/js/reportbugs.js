(function () {
    console.log("reportbugs dzialam");

    grecaptcha.ready(function () {
        grecaptcha.execute('6Ldd3HEUAAAAABwaxh3XsxBxRBrKsmuGIltNKr7S', { action: 'reporting' })
            .then(function (token) {
                console.log(token);
            });
    });

    $("#bug-submit").click(function () {
        var response = $("#bug-report-text").val();
        if (response === "") {
            return;
        }
        $.ajax({
            type: 'POST',
            url: 'api/bugs',
            data: {
                'information': response,
            },
            complete: function (data) {
                console.log(data);
                window.close(this);
            }
        });
    });
})();
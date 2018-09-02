//(function () {

//})();

//$.ajax({
//    url: "/api/getWeather",
//    data: {
//        zipcode: 97201
//    },
//    success: function (result) {
//        $("#weather-temp").html("<strong>" + result + "</strong> degrees");
//    }
//});

function getRandomIdea() {
    console.log("random idea");
}

(function () {
    "use strict";
    console.log("dzialam");


    $("#search-input").click(function () {
        var option = String(Math.floor((Math.random() * 3) + 1));
        var className = "background-variation" + option
        $("body").removeClass();
        $("body").addClass(className);

    });

    $("#description-button").click(function () {
        var panel = $("#description-panel");

        if (panel.hasClass("tooltip-inactive")) {
                panel.removeClass();
                panel.addClass("tooltip-open");
        } else {
            panel.removeClass();
            panel.addClass("tooltip-close");
            panel.delay(1500).queue(function (next) {
                panel.addClass("tooltip-inactive");
                next();
            });
        }

        $(this).toggleClass("description-button-spin");
    });

    $("#refresh-button").click(function () {
        getRandomIdea();
    })

    $(document).keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            getRandomIdea();
        }
    });
})();
//(function () {

//})();

var uri = 'api/ideas/';

function log(message) {
    $("#log").text(message);
    $("#log").scrollTop(0);

}

function getSpecifiedIdea(title) {
    $.ajax({
        type: 'GET',
        url: uri + title,
        success: function (data) {
            console.log(data);
            log(data.title + "  " + data.likes + " + ");
            data.definitions.forEach(function (definition) {
                log(definition.text + "  " + definition.likes + " + ");
            });
        },
        fail: function (data) {
            console.log("fail!");
        }
    });
}

function autocomplete(data) {
    $("#search-input").autocomplete({
        source: data
        //function (request, response) {
        //var uri = 'api/ideas/';
        ////request.term;
        ////console.log(uri);
        //$.ajax({
        //    url: uri,
        //    dataType: "json",
        //    success: function (data) {
        //        response(data);
        //        console.log(data);
        //    },
        //    fail: function () {
        //        console.log("fail");
        //    }
        //});
        //}
        ,
        autoFocus: true,
        minLength: 2,
        delay: 500,
        select: function (event, ui) {
            getSpecifiedIdea(ui.item.value);
        }
    });
}

function getIdeas() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            autocomplete(data);
        },
        fail: function (data) {
            console.log("fail!");
        }
    });
}

function getRandomIdea() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            var rand = Math.floor(Math.random() * data.length);
            console.log(data[rand]);
        },
        fail: function (data) {
            console.log("fail!");
        }
    });
}

(function () {
    "use strict";
    console.log("dzialam");
    getIdeas();

    $("#search-input").on('change keyup paste', function () {
        $(this).val($(this).val().toLowerCase());
    });

    $("#search-input").click(function () {
        var b = $("body");
        do {
            var option = String(Math.floor((Math.random() * 4) + 1));
            var className = "background-variation" + option;
        } while (b.hasClass(className) == true);

        b.removeClass();
        b.addClass(className);
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

        $(this).toggleClass("button-spin");
    });

    $("#description-button").hover(function () {
        $(this).toggleClass("button-spin");
    })

    $("#refresh-button").click(function () {
        getRandomIdea();
    })

    $(document).keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            getRandomIdea();
        }
    });
})();
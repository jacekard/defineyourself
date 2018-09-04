//(function () {

//})();

function log(message) {
    $("<div>").text(message).prependTo("#log");
    $("#log").scrollTop(0);
}

function getIdea() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            console.log(data);
        },
        fail: function (data) {
            console.log("fail!");
        }
    });
}

function getRandomIdea() {
    console.log("random idea");
}

var availableTags = [
    "ActionScript",
    "AppleScript",
    "Asp",
    "BASIC",
    "C",
    "C++",
    "Clojure",
    "COBOL",
    "ColdFusion",
    "Erlang",
    "Fortran",
    "Groovy",
    "Haskell",
    "Java",
    "JavaScript",
    "Lisp",
    "Perl",
    "PHP",
    "Python",
    "Ruby",
    "Scala",
    "Scheme"
];

(function () {
    "use strict";
    console.log("dzialam");

    $("#search-input").autocomplete({
        source: function (request, response) {
            var uri = 'api/ideas/';
            uri += request.term;
            console.log(uri);
            $.ajax({
                url: uri,
                dataType: "json",
                success: function (data) {
                    response(data);
                    console.log(data);
                }
            });
        },
        minLength: 2,
        delay: 100,
        response: function (event, ui) {
            //console.log(ui.content);
        }
    });


    $("#search-input").on('click', 'input', function () {
        var input = $(this);
        console.log(input.text());
        //getIdea();
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
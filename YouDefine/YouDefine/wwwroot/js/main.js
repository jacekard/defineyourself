//(function () {

//})();

var uri = 'api/ideas/';
var ideas = new Array();

function log(message) {
    $("#log").text(message);
    $("#log").scrollTop(0);
}

function specifiedIdea(idea) {
    var title = idea.title;
    var likes = idea.likes;
    var date = idea.lastModifiedDate;
    idea.definitions.forEach(function (def) {
        console.log(def.id + ": " + def.text + "  " + def.likes);
    });
    $("#search-input").autocomplete("search", "");
    $(".search-container").addClass("search-input-hidden");
    $("#idea-likes-count").text(likes);
    $("#idea-info").css({
        "display": "inline-block",
        "opacity": "1"
    });
}

function searchAgain() {
    $("#idea-likes-count").text("");
    $("#idea-likes-icon").toggle();

    $(".search-container").removeClass("search-input-hidden");
    $(".search-container").addClass("search-input-active");
}

function postNewIdea() {

}

function putNewIdea() {

}

function likeDefinition(title, id) {
    var url = uri + "likeDefinition/" + title + "/" + id;
    $.ajax({
        type: 'PUT',
        url: url,
        success: function (data) {
            // display incremented upvotes count by one
        },
    });
}

function getSpecifiedIdea(title) {
    if (title.length < 2) {
        return;
    }
    $.ajax({
        type: 'GET',
        url: uri + title,
        statusCode: {
            204: function () {
                console.log("no one defined " + title + " yet.");
            },
            200: function (data) {
                specifiedIdea(data);
            }
        },
        complete: function (xhr, status) {
           // console.log(xhr + "  " + status);
        }
    });
}

function autocomplete() {
    if (ideas.length == 0) {
        setTimeout(autocomplete, 1);
        return;
    }

    $("#search-input").autocomplete({
        source: ideas,
        minLength: 1,
        classes: {
            "ui-autocomplete": "ui-autocomplete"
        },
        appendTo: "#result-container",
        delay: 1200,
        autoFocus: true,
        select: function (event, ui) {
            getSpecifiedIdea(ui.item.value);
        },
        search: function (event, ui) {
            $("#search-input").autocomplete("close");
            getSpecifiedIdea($("#search-input").val());
        },
        messages: {
            noResults: '',
            results: function () { }
        }
    });
}

function assignData(data) {
    ideas = data;
}

function getIdeas() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            assignData(data);
        },
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
    autocomplete();


    $("#search-input").on('change keyup paste', function () {
        $(this).val($(this).val().toLowerCase());
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
            //getSpecifiedIdea(title);

            //getRandomIdea();
        }
    });

    setInterval(getIdeas, 5000);
})();
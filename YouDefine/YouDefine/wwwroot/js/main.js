//(function () {

//})();

var uri = 'api/ideas/';
var ideas = new Array();
var messages = [
    "you can define it!",
    "maybe it's your turn?",
    "what do you have in mind?",
    "tell us what you think.",
    "is it even a word?",
    "define it, please",
    "press + to add it.",
    "add this to our dict"
];
var isSearchAgain = false;
var ideaMatched = false;

function log(message) {
    $("#log").text(message);
    $("#log").scrollTop(0);
}

function specifiedIdea(idea) {
    isSearchAgain = true;

    var title = idea.title;
    var likes = idea.likes;
    var date = idea.lastModifiedDate;
    idea.definitions.forEach(function (def) {
        console.log(def.id + ": " + def.text + "  " + def.likes);
    });

    $("#search-input").autocomplete("search", "");
    if ($(".search-container").hasClass("search-input-active")) {
        $(".search-container").removeClass("search-input-active");
    }
    $(".search-container").addClass("search-input-hidden");
    $("#idea-likes-count").text(likes);
    $("#idea-date").text(date);
    $(".idea-info").addClass("show-element");
    $(".add-new-panel").removeClass("show-element");
    
}

function searchAgain() {
    $(".idea-info").removeClass("show-element");
    $(".search-container").removeClass("search-input-hidden");
    $(".search-container").addClass("search-input-active");
}

function showNewIdeaPanel(title) {
    var message = messages[Math.floor(Math.random() * messages.length)];
    var titleMsg = "We haven't found";
    $(".add-new-panel").addClass("show-element");
    $("#new-idea-title").text(titleMsg);
    $("#new-idea-message").text(message);
    $("#new-idea").html(title + ", ");
    $("#new-idea").fadeOut();
    $("#new-idea").fadeIn();

}

function postNewIdea(title, text) {
    var url = uri + title + "/" + id;
    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            console.log("added new idea!");
        },
        fail: function (data) {
            console.log("there was some problem");
        }
    });
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
                ideaMatched = false;
                showNewIdeaPanel(title);
            },
            200: function (data) {
                ideaMatched = true;
                specifiedIdea(data);
            }
        },
        complete: function (xhr, status) {
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
        minLength: 2,
        classes: {
            "ui-autocomplete": "ui-autocomplete"
        },
        appendTo: "#result-container",
        delay: 400,
        autoFocus: true,
        select: function (event, ui) {
            getSpecifiedIdea(ui.item.value);
        },
        search: function (event, ui) {
            $(this).autocomplete("close");
            getSpecifiedIdea($("#search-input").val());
            if (ideaMatched) {
                $("ui-autocomplete").css("display", "none");
                $(".ui-menu-item").css("display", none);
            }
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
        var val = $(this).val();
        $(this).val(val.toLowerCase());
        if (val.length < 3) {
            $("#definition-input").css("display", "none");
            $(".add-new-panel").removeClass("show-element");
        }
        if (val == "" && isSearchAgain) {
            isSearchAgain = false;
            searchAgain();
        }
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

    $(".add-new-icon").click(function () {
        $("#definition-input").css("display", "inline-block");
        $(".add-new-panel").removeClass("show-element");
        postNewIdea();
    })

    $(document).keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            //getSpecifiedIdea(title);

            //getRandomIdea();
        }
    });

    setInterval(getIdeas, 5000);
})();
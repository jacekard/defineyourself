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

function createDefinitions(idea, def) {
    var divId = 'def' + def.id;
    var infoId = 'span' + def.id;
    var iconId = 'thumb-up' + def.id;
    var likesId = 'likes' + def.id;

    jQuery('<div/>', {
        class: 'single-definition ' + divId
    }).appendTo("#definition-container");

    jQuery('<div/>', {
        class: 'definition-info ' + infoId
    }).appendTo('.' + divId);

    var likesCount = jQuery('<span/>', {
        class: 'definition-likes-count ' + likesId,
        text: def.likes
    }).appendTo('.' + infoId);

    jQuery('<i/>', {
        class: 'material-icons definition-likes-icon ' + iconId,
        text: 'thumb_up'
    }).prependTo('.' + infoId).on('click', function () {
        likeDefinition(idea, def, likesCount, this);
    });


    jQuery('<div/>', {
        class: 'definition-text',
        text: def.text
    }).appendTo('.' + divId);

    //has to add date from api controller and display it here:
    //jQuery('<div/>', {
    //    class: 'definition-date',
    //    text: def.date
    //}).appendTo('.' + divId);
}

function specifiedIdea(idea) {
    isSearchAgain = true;

    var title = idea.title;
    var likes = idea.likes;
    var date = idea.lastModifiedDate;
    idea.definitions.forEach(function (def) {
        createDefinitions(idea, def);
    });

    $("#search-input").autocomplete("search", "");
    if ($(".search-container").hasClass("search-input-active")) {
        $(".search-container").removeClass("search-input-active");
    }
    $(".search-container").addClass("search-input-hidden");
    $("#idea-likes-count").text(likes);
    $(".idea-date").text(date);
    $(".idea-date").addClass("show-element");
    $(".idea-info").addClass("show-element");
    $(".add-new-panel").removeClass("show-element");
}

function searchAgain() {
    //deletes div, should be animation and then empty();
    $("#definition-container").empty();

    $(".idea-info").removeClass("show-element");
    $(".idea-date").removeClass("show-element");
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

function newIdeaSuccessfullyPosted() {
    $(".add-idea-button").fadeOut();
    $("#definition-input").fadeOut();
    $("#definition-input").css("display", "none");
    $(".add-idea-button").css("display", "none");
}

function postNewIdea(title, text) {
    var url = uri + title + "/" + text;
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

function likeDefinition(idea, def, likesCount, icon) {
    var url = uri + "likeDefinition/" + idea.title + "/" + def.id;
    $.ajax({
        type: 'PUT',
        url: url,
        success: function (data) {
            // display incremented upvotes count by one
            // prevent user from liking it again!
            $(likesCount).text(parseInt(def.likes) + 1);
            $(likesCount).css('color', '#008000');
            $("#idea-likes-count").text(parseInt(idea.likes) + 1);
            $(icon).css('color', '#008000');
            $(icon).off();
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
        minLength: 1,
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
            getSpecifiedIdea($("#search-input").val());
            if (ideaMatched) {
                $("ui-autocomplete").css("display", "none");
                $(".ui-menu-item").css("display", "none");
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
            if (!data[rand]) {
                return;
            }
            $("#search-input").val(data[rand]);
            getSpecifiedIdea(data[rand]);
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
            $(".add-idea-button").css("display", "none");
            $(".add-new-panel").removeClass("show-element");
        }

        // ! ! ! ! NOT DONE YET (NEEDS A BETTER BOOLEAN VARIABLE)
        // must define a state of being "on top" and then disable add-new-panel display
        if (ideaMatched) {
            $(".add-new-panel").removeClass("show-element");
            //$("#search-input").autocomplete("disable");
            //$("#search-input").autocomplete("enable");

        }
        // ! ! ! !


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
    });

    $("#refresh-button").click(function () {
        getRandomIdea();
    });

    $(".add-new-icon").click(function () {
        $(".add-idea-button").fadeIn();
        $("#definition-input").fadeIn();
        $("#definition-input").css("display", "inline-block");
        $(".add-idea-button").css("display", "inline-block");
        $(".add-new-panel").removeClass("show-element");
    });

    $(".add-idea-button").click(function () {
        var title = $("#search-input").val();
        var text = $("#definition-input").val();
        if (title != "" && text != "") {
            postNewIdea(title, text);
        }
    });

    $(document).keypress(function (e) {
        if ((e.which && e.which == 13) || (e.keyCode && e.keyCode == 13)) {
            getRandomIdea();
        }
    });

    setInterval(getIdeas, 5000);
})();
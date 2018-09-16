(function () {
    console.log("about dzialam");

    $("#bug-report-link").click(function () {
        window.open(
            "/reportBugs",
            'targetWindow',
            "toolbar=0, menubar=0, status=0, top=100, left=500, scrollbars=0, resizable=0, width=500px, height=620px");
    });
})();
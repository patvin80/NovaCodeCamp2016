/// <reference path="../App.js" />

(function () {
    "use strict";

    // The Office initialize function must be run each time a new page is loaded
    Office.initialize = function (reason) {
        $(document).ready(function () {
            app.initialize();
            displayItemDetails();
        });
    };

    // Displays the "Subject" and "From" fields, based on the current mail item
    function displayItemDetails() {
        var item = Office.context.mailbox.item;
        // Convert this to a Code Snippet
        $(document).ready(function () {
            $.getJSON("../../api/Values/Get?message=" + item.subject, function () {
                console.log("Request completed");
            })
           .done(function (data) {
               var obj = jQuery.parseJSON(data);
               for (var i = 0; i < obj.document_tone.tone_categories[0].tones.length; i++) {
                   var obj1 = obj.document_tone.tone_categories[0].tones[i];
                   $("#tones tr:last").after('<tr><td>' + obj1.tone_name + '</td><td>' + obj1.score + '</tr>');
               }
           })
            .fail(function (data) {
                console.log("error");
            });
        });
        if (from) {
            $('#from').text(from.displayName);
            $('#from').click(function () {
                app.showNotification(from.displayName, from.emailAddress);
            });
        }
    }
})();
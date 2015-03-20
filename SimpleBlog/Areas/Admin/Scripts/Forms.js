
$(document).ready(function () {
    //search for every 'a' tag which has a 'data-post' on it, add a click handler to them
    $("a[data-post]").click(function(e) {
        e.preventDefault(); //stops the browser trying to navigate to where th link is located

            //Alias out the jquery object of our context into a variable, and get the message out of it
        var $this = $(this);
        var message = $this.data("post");

            //If there is a message, and user clicks no in the Dialog box, then return and don't do anything
        if (message && !confirm(message))
            return;

        //var antiForgeryToken = $("anti-forgery-form").find("input").val();
        //var antiForgeryInput = $("<input type ='hidden' name =''>").val(antiForgeryToken);
        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type ='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        //if no message and user clicks 'ok', then we post to the end point of this link
        $("<form>")      //create a form
            .attr("method", "post")
            .attr("action", $this.attr("href")) //set its action to wherever th link was pointing to
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();  //and submit the form
    });

});
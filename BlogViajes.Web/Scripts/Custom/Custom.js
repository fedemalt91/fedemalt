function AnswerComment(divId) {
    var domDivSender = $("#" + divId);
    var commentSection = $("#sectionResponse");
    $("#userComment").val("Escriba su respuesta...");
    $("#userName").val("Nombre");
    $("#userEmail").val("E-Mail");
    $("#userEmail").css("border", '1px solid #ddd');
     $("#userName").css("border", '1px solid #ddd');
    
    domDivSender.parent().parent().append(commentSection);
    commentSection.show();
}

function textLostFocus(element) {
    if (element.val().trim() == "") {
        element.val("Escriba su respuesta...");
    }

}

function nameLostFocus(element) {
    if (element.val().trim() == "") {
        element.val("Nombre");
    }
}

function emailGainFocus(element) {
    if (element.val().trim() == "E-Mail") {
        element.val("");
        element.css("border", '1px solid #ddd');
    }
}

function emailLostFocus(element) {
    if (element.val().trim() == "") {
        element.val("E-Mail");
    }
}

function nameGainFocus(element) {
    if (element.val().trim() == "Nombre") {
        element.val("");
        element.css("border", '1px solid #ddd');
    }
}

function textGainFocus(element) {
    if (element.val().trim() == "Escriba su respuesta...") {
        element.val("");
        element.css("border", '1px solid #ddd');
    }
}
function sendResponse(element) {
    var parentCommentElement = element.parent().parent();
    var parentCmtId = parentCommentElement.data("cmmtid");
    var parentCont = $("#contentId").val()
    var textCmt = $("#userComment").val();
    var postingUser = $("#userName").val();
    var postingUserEmail = $("#userEmail").val();
    var valid = true;
    if (textCmt == null || textCmt.trim() == '' || textCmt.trim() == 'Escriba su respuesta...') {
        valid = false;
        $("#userComment").css("border", '1px solid red');
    }
    if (postingUserEmail == null || postingUserEmail == '') {
        valid = false;
        $("#userEmail").css("border", '1px solid red');
    }
    else {

        var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
       
         var validEmail =   re.test(email);
        if (!validEmail) {
            $("#userEmail").css("border", '1px solid red');
        }
        valid = validEmail;
    }
    if (valid) {
        $.ajax({
            url: '/Articulos/PostComment',
            type: 'POST',
            async: true,
            data: { parentCommentId: parentCmtId, parentContent: parentCont, texto: textCmt, user: postingUser, email: postingUserEmail },
            success: function (result) {
                $('#sectionResponse').hide();
                $("#userComment").val("Escriba su respuesta...");
                $("#userName").val("Nombre");
                $("#userEmail").val("E-Mail");
                parentCommentElement.append(result);
            },
            error: function (xhr, status) {
                $('#sectionResponse').hide();
                parentCommentElement.append(status);
            }
        });
    }
}

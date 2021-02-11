// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function toggleResetPswd(e) {
    e.preventDefault();
    $('#logreg-forms .form-signin').toggle() // display:block or none
    $('#logreg-forms .form-reset').toggle() // display:block or none
}

function toggleSignUp(e) {
    e.preventDefault();
    $('#logreg-forms .form-signin').toggle(); // display:block or none
    $('#logreg-forms .form-signup').toggle(); // display:block or none
}

function modalGetLink(event) {
    var button = $(event.relatedTarget);
    var id = button.data('id')
    var href = button.find('a').attr('href');
    console.log(button);
    var modal = $(this);
    modal.find('#'+event.data.elementName).attr('action', href);
    modal.find('#id').val(id);
}

$(() => {
    // Login Register Form
    $('#logreg-forms #forgot_pswd').click(toggleResetPswd);
    $('#logreg-forms #cancel_reset').click(toggleResetPswd);
    $('#logreg-forms #btn-signup').click(toggleSignUp);
    $('#logreg-forms #cancel_signup').click(toggleSignUp);
})

function buildBracket(id) {
    $.ajax({
        url: "/Tournaments/GetBracket/",
        data: {id: id},
            success: (data) => {
                var container = $("#bracket");
                $("#bracket").bracket({
                    skipConsolationRound: true,
                    save: saveFn,
                    init: JSON.parse(data)
                });

                $(document).on('click','.team',(event) => {
                    console.log($(event.target).parent(".team").children(".label").html());
                    console.log($(event.target).parent(".team").children(".score").data("resultid").split("-")[1]);
                });
            }
        });
}

function saveFn(data) {
    $.ajax({
        url: "/Tournaments/SaveBracket/",
        data: {bracketJson: JSON.stringify(data)},
    });
}
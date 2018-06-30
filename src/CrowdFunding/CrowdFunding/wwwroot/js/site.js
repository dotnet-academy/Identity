// TODO: handle double-click on button (to avoid multiple form submissions).
$('#submit-project').on('click', function (event) {
    event.preventDefault();

    let formData = $('#project-create-form').serialize();
    
    $.ajax({
        url: '/Projects/Create',
        type: 'post',
        data: formData
    }).done(function () {
        $('#success-created').removeClass('hidden');
    }).fail(function () {
        $('#fail-created').removeClass('hidden');
    }).always(function () {
        $('#project-create-form').hide();
    });
});
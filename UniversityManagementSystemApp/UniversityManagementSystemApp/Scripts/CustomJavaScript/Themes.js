
$(document).ready(function () {

    $('#colorId').change(function () {
        if ($('#colorId option:selected').text() == "Select Themes") {
            $('body').css({
                'background': 'white',
                'color': 'black'
            });
        }
        else if ($('#colorId option:selected').text() == "Dark") {
            $('body').css({
                'background': '#1E1E1E',
                'color': 'white'
            });
        }
        else if ($('#colorId option:selected').text() == "Light") {
            $('body').css({
                'background': '#D6DBE9',
                'color': 'black'
            });
        }
    });

});

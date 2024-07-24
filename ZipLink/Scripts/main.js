$(document).ready(function () {

    $(".shortenUrlForm").submit(function (e) {
        e.preventDefault();
        var action = $(this).attr("action"); // Get form action URL
        $.ajax({
            type: "POST",
            url: action,
            crossDomain: true,
            data: new FormData(this),
            dataType: "json",
            processData: false,
            contentType: false,
            headers: {
                "Accept": "application/json",
                
            },
            beforeSend: function () {
                $(".generatedUrl").css("display","none")
                startLoader(); // Start loader before sending the request
            },
            success: function (success) {
                $(".generatedUrl").html(success.shortenurl);
            },
            complete: function (success) {
                setTimeout(function () {
                    $('#loader').toggle();
                    $('#loader').css('width', '0').hide();
                    setTimeout(function () {
                        $(".generatedUrl").toggle()
                    }, 100)
                }, 2100);// Stop loader after the request is complete
                
            }
        })
        // Function to show and start the loader animation

    })
})
function startLoader() {
    $('#loader').toggle();
        $('#loader').css('width', '0').show();
        $('#loader::before').css('animation', 'none'); // Reset animation
        setTimeout(function () {
            $('#loader').css({
                'width': '100%',
                'transition': 'width 1s linear'
            });
        }, 10); // Delay to ensure the animation starts
    }

    // Function to stop the loader animation
    function stopLoader() {
        setTimeout(function () {
            $('#loader').toggle();
            $('#loader').css('width', '0').hide();
        }, 2100); // Slight delay to ensure the loader completes before hiding
    }

var apiURL ='https://localhost:44327/';

$.ajaxSetup({
    headers: {
        Authorization: 'Bearer ' + localStorage.getItem("user"),
        'Accept': 'application/json',
    },
    beforeSend: function (xhr) {
        xhr.setRequestHeader("Accept", "application/vvv.website+json;version=1");
    }
});

$(document).ready(function() {
    $(".show-password").click(function() {
        let type = $("#password").attr("type");
        type === "text" ? $("#password").attr("type", "password") : $("#password").attr("type", "text");
    });

    $("#btn-sign-in").click(function() {
        let email = $("#email").val().trim();
        let password = $("#password").val().trim();
     
        var Url = apiURL + 'api/Auth/SignIn';
        if(email === '' || password === '') {

        } else {
            
            loginCredentials = {
                email: email,
                passwordHash: password
            }
            $.ajax({
                url: Url,
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(loginCredentials),
                success: function (result) {
                    if(result.success) {
                        localStorage.removeItem("user");
                        window.localStorage.setItem("user", result.token);
                        window.open("search.html" , "_self");
                      
                       
                    } else {

                    }
                },
            });
        }
    });
});


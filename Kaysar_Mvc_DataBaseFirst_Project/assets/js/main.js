function logoutmenu(){
    document.getElementById("logoutmenu").classList.toggle('logoutmenuactive');
}

function sidemenu(){
    windowSizeCondition();
}

function windowSizeCondition(){

    if ($(window).width() > 1024) {
        document.getElementById("sidebar").classList.toggle('sidebar1024px');
        document.getElementById("main").classList.toggle('main1024px');
        document.getElementById("dropbar").classList.toggle('dropbar1024px');
    }

    else if ($(window).width() > 768  &&  $(window).width() <= 1024) {
        document.getElementById("sidebar").classList.toggle('sidebar768px');
        document.getElementById("main").classList.toggle('main768px');
        document.getElementById("dropbar").classList.toggle('dropbar768px');
    }

    else if ($(window).width() <= 768) {

        document.getElementById("sidebar").classList.toggle('sidebarsmsize');
        document.getElementById("main").classList.toggle('mainsmsize');
        document.getElementById("dropbar").classList.toggle('dropbarsmsize');
    }
}


function purchasefunction(){
    var purchasemanu = document.getElementById("purchasemanu").className

    if(purchasemanu == 'purchasemanu'){
        document.getElementById("purchasemanu").className = "purchasemanuActive";
    }
    else{
        document.getElementById("purchasemanu").className = "purchasemanu";
    }
    document.getElementById("salesmanu").className = "salesmanu";
    document.getElementById("empmanu").className = "empmanu";
}


function salesfunction(){
    var salesmanu = document.getElementById("salesmanu").className

    if(salesmanu == 'salesmanu'){
        document.getElementById("salesmanu").className = "salesmanuActive";
    }
    else{
        document.getElementById("salesmanu").className = "salesmanu";
    }
    document.getElementById("purchasemanu").className = "purchasemanu";
    document.getElementById("empmanu").className = "empmanu";
}

function empfunction(){
    var empmanu = document.getElementById("empmanu").className

    if(empmanu == 'empmanu'){
        document.getElementById("empmanu").className = "empmanuActive";
    }
    else{
        document.getElementById("empmanu").className = "empmanu";
    }
    document.getElementById("purchasemanu").className = "purchasemanu";
    document.getElementById("salesmanu").className = "salesmanu";
}


"use strict";
$(document).ready(function(){
    $(".md-form-control").each(function() {
        $(this).parent().append('<span class="md-line"></span>');
    });
    $(".md-form-control").change(function() {
        if ($(this).val() == "") {
            $(this).removeClass("md-valid");
        } else {
            $(this).addClass("md-valid");
        }
    });

});






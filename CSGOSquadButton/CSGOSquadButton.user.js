// ==UserScript==
// @name         CSGOSquad Button
// @namespace    rakijah
// @version      0.2
// @description  Adds a CSGOSquad Button to Steam Profile pages
// @author       rakijah
// @grant        none
// @include      /^https?:\/\/steamcommunity.com\/id\/[^\/]*\/?$/
// @include      /^https?:\/\/steamcommunity\.com\/profiles\/([0-9]*)\/?$/
// @require      http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js
// ==/UserScript==
/* jshint -W097 */
'use strict';

$(document).ready( function() {
    var p_id = new RegExp("^https?:\/\/steamcommunity.com\/id\/([^\/]*)\/?$");
    var p_profile = new RegExp("^https?:\/\/steamcommunity\.com\/profiles\/([0-9]*)\/?$");
    var url = window.location.href;
    var target = "http://csgosquad.com/search/";
    var result = p_id.exec(url);
    if(result === null) {
        result = p_profile.exec(url);
        if(result === null)
            return;
    }
    target += result[1];
    var button_html = '<a href="' + target + '" target="_blank" class="btn_profile_action btn_medium"><span>CS:GO Squad</span></a>';
    var element = document.getElementsByClassName("profile_header_actions")[0];
    element.insertAdjacentHTML('beforeend', button_html);
});
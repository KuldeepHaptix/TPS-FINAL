/* Preload images script */
var myimages = new Array()

function preloadimages() {
    for (i = 0; i < preloadimages.arguments.length; i++) {
        myimages[i] = new Image();
        myimages[i].src = preloadimages.arguments[i];
    }
}
var thisDate = 1;
var wordMonth = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
var today = new Date();
var todaysDay = today.getDay() + 1;
var todaysDate = today.getDate();
var todaysMonth = today.getUTCMonth() + 1;//7;//today.getUTCMonth() + 1			
var todaysYear = today.getFullYear();
var monthNum = todaysMonth;
var yearNum = todaysYear;
var firstDate = new Date(String(monthNum) + "/1/" + String(yearNum));
var firstDay = firstDate.getUTCDay();
var lastDate = new Date(String(monthNum + 1) + "/0/" + String(yearNum));
var numbDays = 0;
var calendarString = "";
var eastermonth = 0;
var easterday = 0;
var selectMonth = "";

function changedate(buttonpressed, monthno, yearno) {
   
    document.getElementById("Setholiday").style.display = "none";
    if (buttonpressed == "prevyr") yearNum--;
    else if (buttonpressed == "nextyr") yearNum++;
    else if (buttonpressed == "prevmo") monthNum--;
    else if (buttonpressed == "nextmo") monthNum++;
    else if (buttonpressed == "gotomo") {
        monthNum = monthno;
        yearNum = yearno;
    }
    else if (buttonpressed == "return") {
        monthNum = todaysMonth;
        yearNum = todaysYear;
    }
    if (monthNum == 0) {
        monthNum = 12;
        yearNum--;
    }
    else if (monthNum == 13) {
        monthNum = 1;
        yearNum++
    }
    document.getElementById("setupApplied").innerText = "Applied default Holiday Setup for " + wordMonth[monthNum - 1] + "-" + yearNum ;
    selectMonth = monthNum + "@" + yearNum;
    //lastDate = new Date(String(monthNum+1)+"/0/"+String(yearNum));
    lastDate = new Date(yearNum, monthNum, 0);
    numbDays = lastDate.getDate();
    firstDate = new Date(String(monthNum) + "/1/" + String(yearNum));
    firstDay = firstDate.getDay() + 1;
    createCalendar();
    return;
}


function easter(year) {
    var a = year % 19;
    var b = Math.floor(year / 100);
    var c = year % 100;
    var d = Math.floor(b / 4);
    var e = b % 4;
    var f = Math.floor((b + 8) / 25);
    var g = Math.floor((b - f + 1) / 3);
    var h = (19 * a + b - d - g + 15) % 30;
    var i = Math.floor(c / 4);
    var j = c % 4;
    var k = (32 + 2 * e + 2 * i - h - j) % 7;
    var m = Math.floor((a + 11 * h + 22 * k) / 451);
    var month = Math.floor((h + k - 7 * m + 114) / 31);
    var day = ((h + k - 7 * m + 114) % 31) + 1;
    eastermonth = month;
    easterday = day;
}
function createCalendar() {
    calendarString = '';
    var daycounter = 0;
    // calendarString += '<div>';
    // calendarString += '<div  style=\"float:left;padding-right: 20px;\"><a onMouseOver=\"document.PrevYr.src=\'images\/prev_yr_hover\.png\';\" onMouseOut=\"document.PrevYr.src=\'images\/prev_yr\.png\';\" onClick=\"changedate(\'prevyr\')\"><img name=\"PrevYr\" src=\"images\/prev_yr\.png\" width=\"40\" height=\"40\" border=\"0\" alt=\"Prev Yr\"\/><\/a><\/div>';
    // calendarString += '<div  style=\"float:left;\"><a curser onMouseOver=\"document.PrevMo.src=\'images\/Prev_hover\.png\';\" onMouseOut=\"document.PrevMo.src=\'images\/prev\.png\';\" onClick=\"changedate(\'prevmo\')\"><img name=\"PrevMo\" src=\"images\/prev\.png\" width=\"20\" height=\"40\" border=\"0\" alt=\"Prev Mo\"\/><\/a><\/div>';
    // calendarString += '<div  style=\"float:left;width:150px;text-align:center;\"><b style=\"line-height: 40px; text-transform: uppercase; font-family: georgia; font-size: 15px;\">' + wordMonth[monthNum-1] + '&nbsp;&nbsp;' + yearNum + '<\/b><\/div>';
    // calendarString += '<div  style=\"float:left;\"><a  onMouseOver=\"document.NextMo.src=\'images\/next_hover\.png\';\" onMouseOut=\"document.NextMo.src=\'images\/Next\.png\';\" onClick=\"changedate(\'nextmo\')\"><img name=\"NextMo\" src=\"images\/Next\.png\" width=\"20\" height=\"40\" border=\"0\" alt=\"Next Mo\"\/><\/a><\/div>';
    // calendarString += '<div  style=\"float:left;padding-left: 20px;\"><a onMouseOver=\"document.NextYr.src=\'images\/next_yr_hover\.png\';\" onMouseOut=\"document.NextYr.src=\'images\/next_yr\.png\';\" onClick=\"changedate(\'nextyr\')\"><img name=\"NextYr\" src=\"images\/next_yr\.png\" width=\"40\" height=\"40\" border=\"0\" alt=\"Next Yr\"\/><\/a><\/div>';
    // calendarString += '<\/div><br/\>';
    calendarString += '<div class=\"head\">'
    calendarString += '<a class=\"prev mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\" onClick=\"changedate(\'prevmo\')\" href=\"javascript:void(0)\">&lsaquo;</a>'
    calendarString += '<a class=\"next mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\" onClick=\"changedate(\'nextmo\')\" href=\"javascript:void(0)\">&rsaquo;</a>'
    //calendarString += '<a class=\"prev mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\" onClick=\"changedate(\'prevyr\')\" href=\"javascript:void(0)\">&laquo;</a><a class=\"prev mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\" onClick=\"changedate(\'prevmo\')\" href=\"javascript:void(0)\">&lsaquo;</a>'
    //calendarString += '<a class=\"next mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\" onClick=\"changedate(\'nextyr\')\" href=\"javascript:void(0)\">&raquo;</a><a class=\"next mdl-button mdl-js-button mdl-button--fab mdl-button--mini-fab mdl-js-ripple-effect mdl-button--colored\" onClick=\"changedate(\'nextmo\')\" href=\"javascript:void(0)\">&rsaquo;</a>'
    //calendarString += '<h4>April 2014</h4>'
    calendarString += '<h4 style="font-size: 25px !important;color: #a8adb7;font-weight: bold;">' + wordMonth[monthNum - 1] + '&nbsp;&nbsp;' + yearNum + '<\/h4>'
    calendarString += '</div>';
    calendarString += '<div class=\"table\"><table>';
    calendarString += '<tr>';
    calendarString += '<th class=\"col-1\">SUN<\/th>';
    calendarString += '<th class=\"col-2\">MON<\/th>';
    calendarString += '<th class=\"col-3\">TUE<\/th>';
    calendarString += '<th class=\"col-4\">WED<\/th>';
    calendarString += '<th class=\"col-5\">THU<\/th>';
    calendarString += '<th class=\"col-6\">FRI<\/th>';
    calendarString += '<th class=\"col-7\">SAT<\/th>';
    calendarString += '<\/tr>';
    thisDate == 1;
    for (var i = 1; i <= 6; i++) {
        calendarString += '<tr>';
        for (var x = 1; x <= 7; x++) {
            daycounter = (thisDate - firstDay) + 1;
            thisDate++;
            if ((daycounter > numbDays) || (daycounter < 1)) {
                calendarString += '<td class=\"col-' + x + ' disable\"><div>&nbsp;<\/div><\/td>';
            } else {
                var message = checkevents(daycounter, monthNum, yearNum, i, x);
                if (message.length != 0 || ((todaysDay == x) && (todaysDate == daycounter) && (todaysMonth == monthNum) && (todaysYear == yearNum))) {
                    //if ((todaysDay == x)&& (todaysDate == daycounter) && (todaysMonth == monthNum) && (todaysYear == yearNum)) {
                    //	calendarString += '<td class=\"col-'+ x +' archival\"><div>' + daycounter + '<\/div><\/td>';
                    //}
                    if (message.length == 0 && (todaysDay == x) && (todaysDate == daycounter) && (todaysMonth == monthNum) && (todaysYear == yearNum)) {
                        //calendarString += '<td class=\"col-' + x + '\"><div>' + daycounter + '<\/div><\/td>';
                        calendarString += '<td style="background:aliceblue"><a id=$index onclick=OnAClick(this) href="#?date=' + daycounter + '/' + monthNum + '/' + yearNum + '" ><div>' + daycounter + '<\/div></a><\/td>';

                    }
                    else {
                        var textlable = "";
                        try {
                            textlable = document.getElementById("lable").innerHTML;
                        } catch (ex) { }
                        textlable = textlable.trim();

                        if (textlable == "Holiday Setup") {
                            //var holidayType = message.split("@@");
                            //if (holidayType[1] == "1")
                            //{
                            //    calendarString += '<td class=\"col-' + x + ' upcoming\" ><a id=$index onclick=OnAClick(this) href="#?date=' + daycounter + '/' + monthNum + '/' + yearNum + '&rea=' + message + '" ><div style="background:red">' + daycounter + '<\/div></a><\/td>';
                            //}
                            //else if (holidayType[1] == "2") {
                            //    calendarString += '<td class=\"col-' + x + ' upcoming\" ><a id=$index onclick=OnAClick(this) href="#?date=' + daycounter + '/' + monthNum + '/' + yearNum + '&rea=' + message + '" ><div style="background:green">' + daycounter + '<\/div></a><\/td>';
                            //}
                            //else
                            //{ calendarString += '<td class=\"col-' + x + ' upcoming\" ><a id=$index onclick=OnAClick(this) href="#?date=' + daycounter + '/' + monthNum + '/' + yearNum + '&rea=' + message + '" ><div style="background:blue">' + daycounter + '<\/div></a><\/td>'; }
                            
                            calendarString += '<td class=\"col-' + x + ' upcoming\"><a id=$index onclick=OnAClick(this) href="#?date=' + daycounter + '/' + monthNum + '/' + yearNum + '&rea=' + message + '" ><div>' + daycounter + '<\/div></a><\/td>';
                        }
                        else {
                            calendarString += '<td class=\"col-' + x + ' upcoming\"><a><div>' + daycounter + '<\/div></a><\/td>';
                        }

                    }
                } else {
                    // calendarString += '<td class=\"col-' + x + '\"><div>' + daycounter + '<\/div><\/td>';
                    calendarString += '<td style="background:aliceblue"><a id=$index onclick=OnAClick(this) href="#?date=' + daycounter + '/' + monthNum + '/' + yearNum + '" ><div>' + daycounter + '<\/div></a><\/td>';
                }
            }
        }
        calendarString += '<\/tr>';
    }
    //calendarString += '<tr><td colspan=\"7\" nowrap align=\"center\" valign=\"center\" bgcolor=\"#FFFFFF\" width=\"280\" height=\"22\"><a class=\"mdl-button mdl-js-button mdl-js-ripple-effect bmargin_10 tmargin_10\" href=\"javascript:changedate(\'return\')\"><b>Show Today\'s Date<\/b><\/a><\/td><\/tr><\/table></div>';

    var object = document.getElementById('calendar');
    object.innerHTML = calendarString;
    thisDate = 1;
}


function checkevents(day, month, year, week, dayofweek) {
    var numevents = 0;
    var floater = 0;
    var mesage = "";
    for (var i = 0; i < events.length; i++) {
        if (events[i][0] == "W") {
            if ((events[i][2] == dayofweek)) numevents++;
        }
        else if (events[i][0] == "Y") {
            if ((events[i][2] == day) && (events[i][1] == month)) numevents++;
        }
        else if (events[i][0] == "F") {
            if ((events[i][1] == 3) && (events[i][2] == 0) && (events[i][3] == 0)) {
                easter(year);
                if (easterday == day && eastermonth == month) numevents++;
            } else {
                floater = floatingholiday(year, events[i][1], events[i][2], events[i][3]);
                if ((month == 5) && (events[i][1] == 5) && (events[i][2] == 4) && (events[i][3] == 2)) {
                    if ((floater + 7 <= 31) && (day == floater + 7)) {
                        numevents++;
                    } else if ((floater + 7 > 31) && (day == floater)) numevents++;
                } else if ((events[i][1] == month) && (floater == day)) numevents++;
            }
        }
        else {
            var compstr = events[i];
            var strarr = compstr.toString().split("@@VAL##");
            var daycomp = strarr[2];
            var monthcomp = strarr[1];
            var yearcomp = strarr[3];
            if ((daycomp == day) && (monthcomp == month) && (yearcomp == year)) {
                numevents++;
                mesage = strarr[4];
            }
        }
    }
    if (numevents == 0) {
        return mesage;
    } else {
        return mesage;
    }
}
function showevents(day, month, year, week, dayofweek) {
    var eventsid = 'showevents-' + day + '-' + month + '-' + year + '-' + week + '-' + dayofweek + '';
    var theevent = "";
    var floater = 0;
    for (var i = 0; i < events.length; i++) {
        // First we'll process recurring events (if any):
        if (events[i][0] != "") {
            if (events[i][0] == "D") {
            }
            if (events[i][0] == "W") {
                if ((events[i][2] == dayofweek)) {
                    // theevent += "<div style=\"font-family: georgia;float: left; text-align: center; background-color: rgb(176, 42, 26); color: rgb(255, 255, 255); font-weight: bold; padding: 0px 10px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
                    // theevent += '<h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
                    // theevent += '<div>' + events[i][4] + '</\div><br/\><br/\>';
                    theevent += '<div class=\"tooltip\"><div class=\"holder\"><h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">Lincoln High School<\/span><\/p>';
                    theevent += '<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident similique.<\/p>';
                    theevent += '<\/div><\/div>';
                    document.getElementById(eventsid).innerHTML = theevent;
                }
            }
            if (events[i][0] == "M") {
            }
            if (events[i][0] == "Y") {
                if ((events[i][2] == day) && (events[i][1] == month)) {
                    // theevent += "<div style=\"font-family: georgia;float: left; text-align: center; background-color: rgb(176, 42, 26); color: rgb(255, 255, 255); font-weight: bold; padding: 0px 10px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
                    // theevent += '<h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
                    // theevent += '<div>' + events[i][4] + '</\div><br/\><br/\>';
                    theevent += '<div class=\"tooltip\"><div class=\"holder\"><h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">Lincoln High School<\/span><\/p>';
                    theevent += '<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident similique.<\/p>';
                    theevent += '<\/div><\/div>';
                    document.getElementById(eventsid).innerHTML = theevent;
                }
            }
            if (events[i][0] == "F") {
                if ((events[i][1] == 3) && (events[i][2] == 0) && (events[i][3] == 0)) {
                    if (easterday == day && eastermonth == month) {
                        // theevent += "<div style=\"font-family: georgia;float: left; text-align: center; background-color: rgb(176, 42, 26); color: rgb(255, 255, 255); font-weight: bold; padding: 0px 10px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
                        // theevent += '<h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
                        // theevent += '<div>' + events[i][4] + '</\div><br/\><br/\>';
                        theevent += '<div class=\"tooltip\"><div class=\"holder\"><h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">Lincoln High School<\/span><\/p>';
                        theevent += '<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident similique.<\/p>';
                        theevent += '<\/div><\/div>';
                        document.getElementById(eventsid).innerHTML = theevent;
                    }
                } else {
                    floater = floatingholiday(year, events[i][1], events[i][2], events[i][3]);
                    if ((month == 5) && (events[i][1] == 5) && (events[i][2] == 4) && (events[i][3] == 2)) {
                        if ((floater + 7 <= 31) && (day == floater + 7)) {
                            // theevent += "<div style=\"font-family: georgia;float: left; text-align: center; background-color: rgb(176, 42, 26); color: rgb(255, 255, 255); font-weight: bold; padding: 0px 10px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
                            // theevent += '<h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
                            // theevent += '<div>' + events[i][4] + '</\div><br/\><br/\>';
                            theevent += '<div class=\"tooltip\"><div class=\"holder\"><h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">Lincoln High School<\/span><\/p>';
                            theevent += '<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident similique.<\/p>';
                            theevent += '<\/div><\/div>';
                            document.getElementById(eventsid).innerHTML = theevent;
                        } else if ((floater + 7 > 31) && (day == floater)) {
                            // theevent += "<div style=\"font-family: georgia;float: left; text-align: center; background-color: rgb(176, 42, 26); color: rgb(255, 255, 255); font-weight: bold; padding: 0px 10px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
                            // theevent += '<h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
                            // theevent += '<div>' + events[i][4] + '</\div><br/\><br/\>';
                            theevent += '<div class=\"tooltip\"><div class=\"holder\"><h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">Lincoln High School<\/span><\/p>';
                            theevent += '<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident similique.<\/p>';
                            theevent += '<\/div><\/div>';
                            document.getElementById(eventsid).innerHTML = theevent;
                        }
                    } else if ((events[i][1] == month) && (floater == day)) {
                        // theevent += "<div style=\"font-family: georgia;float: left; text-align: center; background-color: rgb(176, 42, 26); color: rgb(255, 255, 255); font-weight: bold; padding: 0px 10px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
                        // theevent += '<h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
                        // theevent += '<div>' + events[i][4] + '</\div><br/\><br/\>';
                        theevent += '<h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">Lincoln High School<\/span><\/p>';
                        theevent += '<p>At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident similique.<\/p>';
                        //theevent += '<\/div><\/div>';
                        document.getElementById(eventsid).innerHTML = theevent;
                    }
                }
            }
        }
        else if ((events[i][2] == day) && (events[i][1] == month) && (events[i][3] == year)) {
            // theevent += "<div class=\"events_bgcolor\" style=\"font-family: georgia;float: left; text-align: center;font-weight: bold; width: 85px; border-radius: 5px; margin-right: 20px;\"><span>" + wordMonth[month-1] +'<\/span><br><span style=\"font-size: 35px; line-height: 35px;\">'+ day +'<\/span><br/\><span>'+ year + '</\span><br/\></\div>';
            // theevent += '<div style=\"float:left\"><h3 style=\"font-size: 16px;margin:0px;\">'+ events[i][5] + '</\h3>';
            // theevent += '' + events[i][4] + '</\div>';
            // theevent += '<a style=\"float:right\">' + events[i][6] + '</\a><br/\><br/\><br/\><br/\><br/\>';
            theevent += '<h4>' + events[i][5] + '<\/h4><p class=\"info-line\"><span class=\"time\">' + day + '/' + month + '/' + year + '<\/span><span class=\"place\">' + events[i][6] + '<\/span><\/p>';
            theevent += '<p>' + events[i][4] + '<\/p>';
            document.getElementById(eventsid).innerHTML = theevent;
        }
    }
    if (theevent == "") document.getElementById(eventsid).innerHTML = 'No events to show.';
}

function floatingholiday(targetyr, targetmo, cardinaloccurrence, targetday) {
    var firstdate = new Date(String(targetmo) + "/1/" + String(targetyr));	// Object Storing the first day of the current month.
    var firstday = firstdate.getUTCDay();	// The first day (0-6) of the target month.
    var dayofmonth = 0;	// zero out our calendar day variable.
    targetday = targetday - 1;
    if (targetday >= firstday) {
        cardinaloccurrence--;	// Subtract 1 from cardinal day.
        dayofmonth = (cardinaloccurrence * 7) + ((targetday - firstday) + 1);
    } else {
        dayofmonth = (cardinaloccurrence * 7) + ((targetday - firstday) + 1);
    }
    return dayofmonth;
}
var dataJson;

function formatNum(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
}

function initDashboard(dashCode) {
    /*localhost:4554/*/
    var urlToPost = window.location.url;

    setInterval(fetchData, 90000);

    function fetchData() {

        $.getJSON("/api/dashboard/" + dashCode, function (data) {
            console.log(data);
            dataJson = data;
            $('.buildsMonitored').text(data.RandomData[7].m_Item2);
            $('.badgesEarned').text(data.RandomData[5].m_Item2);
            $('.peopleInvolved').text(data.RandomData[6].m_Item2);


            var array = data.RandomData[0].m_Item2.split('-');

            for (var i = 0; i < 6; i++) {
                if (typeof array[i] !== 'undefined') {
                    array[i] = parseInt(array[i]);
                    if (array[i] > 0 && i === 1) {
                        array[i] = array[i] - 1;
                    }
                } else {
                    array[i] = 0;
                }
            }

            console.log(array);
            $('#countdown').html('');
            $('#countdown').countup({
                start: new Date(array[0], array[1], array[2], array[3], array[4], array[5]) //year, month, day, hour, min, sec
            });


            var barChartData = {
                labels: [data.CheckinsByHour[0].m_Item1 + "h", data.CheckinsByHour[1].m_Item1 + "h", data.CheckinsByHour[2].m_Item1 + "h", data.CheckinsByHour[3].m_Item1 + "h", data.CheckinsByHour[4].m_Item1 + "h", data.CheckinsByHour[5].m_Item1 + "h", data.CheckinsByHour[6].m_Item1 + "h", data.CheckinsByHour[7].m_Item1 + "h", data.CheckinsByHour[8].m_Item1 + "h", data.CheckinsByHour[9].m_Item1 + "h", data.CheckinsByHour[10].m_Item1 + "h", data.CheckinsByHour[11].m_Item1 + "h", data.CheckinsByHour[12].m_Item1 + "h", data.CheckinsByHour[13].m_Item1 + "h", data.CheckinsByHour[14].m_Item1 + "h", data.CheckinsByHour[15].m_Item1 + "h", data.CheckinsByHour[16].m_Item1 + "h", data.CheckinsByHour[17].m_Item1 + "h", data.CheckinsByHour[18].m_Item1 + "h", data.CheckinsByHour[19].m_Item1 + "h", data.CheckinsByHour[20].m_Item1 + "h", data.CheckinsByHour[21].m_Item1 + "h", data.CheckinsByHour[22].m_Item1 + "h", data.CheckinsByHour[23].m_Item1 + "h"],
                datasets: [
                    {
                        fillColor: "rgba(48,165,255,0.5)",
                        strokeColor: "rgba(48,165,255,0.8)",
                        highlightFill: "rgba(48,165,255,0.75)",
                        highlightStroke: "rgba(48,165,255,1)",
                        label: " Checkins",
                        data: [data.CheckinsByHour[0].m_Item2, data.CheckinsByHour[1].m_Item2, data.CheckinsByHour[2].m_Item2, data.CheckinsByHour[3].m_Item2, data.CheckinsByHour[4].m_Item2, data.CheckinsByHour[5].m_Item2, data.CheckinsByHour[6].m_Item2, data.CheckinsByHour[7].m_Item2, data.CheckinsByHour[8].m_Item2, data.CheckinsByHour[9].m_Item2, data.CheckinsByHour[10].m_Item2, data.CheckinsByHour[11].m_Item2, data.CheckinsByHour[12].m_Item2, data.CheckinsByHour[13].m_Item2, data.CheckinsByHour[14].m_Item2, data.CheckinsByHour[15].m_Item2, data.CheckinsByHour[16].m_Item2, data.CheckinsByHour[17].m_Item2, data.CheckinsByHour[18].m_Item2, data.CheckinsByHour[19].m_Item2, data.CheckinsByHour[20].m_Item2, data.CheckinsByHour[21].m_Item2, data.CheckinsByHour[22].m_Item2, data.CheckinsByHour[23].m_Item2]
                    }
                ]

            };

            var chart = document.getElementById("bar-chart").getContext("2d");
            window.myBar = new Chart(chart).Bar(barChartData, {
                responsive: true,
                tooltipTemplate: function (valuesObject) {
                    // do different things here based on whatever you want;
                    return valuesObject.value + valuesObject.datasetLabel;
                }
            });


            var barChartData2 = {
                labels: [data.MonthlyScore[0].m_Item1, data.MonthlyScore[1].m_Item1, data.MonthlyScore[2].m_Item1, data.MonthlyScore[3].m_Item1, data.MonthlyScore[4].m_Item1, data.MonthlyScore[5].m_Item1, data.MonthlyScore[6].m_Item1, data.MonthlyScore[7].m_Item1, data.MonthlyScore[8].m_Item1, data.MonthlyScore[9].m_Item1, data.MonthlyScore[10].m_Item1, data.MonthlyScore[11].m_Item1],
                datasets: [
                    {
                        fillColor: "rgba(249,36,63,0.5)",
                        strokeColor: "rgba(249,36,63,0.8)",
                        highlightFill: "rgba(249,36,63,0.75)",
                        highlightStroke: "rgba(249,36,63,1)",
                        label: " BuildCoins",
                        data: [data.MonthlyScore[0].m_Item2, data.MonthlyScore[1].m_Item2, data.MonthlyScore[2].m_Item2, data.MonthlyScore[3].m_Item2, data.MonthlyScore[4].m_Item2, data.MonthlyScore[5].m_Item2, data.MonthlyScore[6].m_Item2, data.MonthlyScore[7].m_Item2, data.MonthlyScore[8].m_Item2, data.MonthlyScore[9].m_Item2, data.MonthlyScore[10].m_Item2, data.MonthlyScore[11].m_Item2]
                    }
                ]

            };

            var auxBuildCoins = data.MonthlyScore[0].m_Item2 + data.MonthlyScore[1].m_Item2 + data.MonthlyScore[2].m_Item2 + data.MonthlyScore[3].m_Item2 + data.MonthlyScore[4].m_Item2 + data.MonthlyScore[5].m_Item2 + data.MonthlyScore[6].m_Item2 + data.MonthlyScore[7].m_Item2 + data.MonthlyScore[8].m_Item2 + data.MonthlyScore[9].m_Item2 + data.MonthlyScore[10].m_Item2 + data.MonthlyScore[11].m_Item2;
            $('.buildcoins').text(formatNum(auxBuildCoins));


            var chart2 = document.getElementById("bar-chart2").getContext("2d");
            window.myBar2 = new Chart(chart2).Line(barChartData2, {
                responsive: true,
                tooltipTemplate: function (valuesObject) {
                    // do different things here based on whatever you want;
                    return valuesObject.value + valuesObject.datasetLabel;
                }
            });


            aux = data.TopBuilds[0].BuildDate.replace('T', ' ');
            aux = aux.split('.');
            aux = aux[0];

            $('.topbuild1 .topBuildHeader').html('<span class="large">' + data.TopBuilds[0].Name + '</span>');
            $('.topbuild1 .buildcoins').html('<h3 class="pull-right"><i class="glyphicon glyphicon-bitcoin glyphicon-s color-white"></i> ' + formatNum(data.TopBuilds[0].Score) + '</h3>');
            $('.topbuild1 .dt').html('<span class="mt10">' + aux + '</span>');

            aux = data.TopBuilds[1].BuildDate.replace('T', ' ');
            aux = aux.split('.');
            aux = aux[0];

            $('.topbuild2 .topBuildHeader').html('<span class="large">' + data.TopBuilds[1].Name + '</span>');
            $('.topbuild2 .buildcoins').html('<h3 class="pull-right"><i class="glyphicon glyphicon-bitcoin glyphicon-s color-white"></i> ' + formatNum(data.TopBuilds[1].Score) + '</h3>');
            $('.topbuild2 .dt').html('<span class="mt10">' + aux + '</span>');
          
            aux = data.TopBuilds[2].BuildDate.replace('T', ' ');
            aux = aux.split('.');
            aux = aux[0];

            $('.topbuild3 .topBuildHeader').html('<span class="large">' + data.TopBuilds[2].Name + '</span>');
            $('.topbuild3 .buildcoins').html('<h3 class="pull-right"><i class="glyphicon glyphicon-bitcoin glyphicon-s color-white"></i> ' + formatNum(data.TopBuilds[2].Score) + '</h3>');
            $('.topbuild3 .dt').html('<span class="mt10">' + aux + '</span>');

            aux = data.BottomBuilds[0].BuildDate.replace('T', ' ');
            aux = aux.split('.');
            aux = aux[0];

            $('.bottombuild1 .topBuildHeader').html('<span class="large">' + data.BottomBuilds[0].Name + '</span>');
            $('.bottombuild1 .buildcoins').html('<h3 class="pull-right"><i class="glyphicon glyphicon-bitcoin glyphicon-s color-white"></i> ' + formatNum(data.BottomBuilds[0].Score) + '</h3>');
            $('.bottombuild1 .dt').html('<span class="mt10">' + aux + '</span>');
            aux = data.BottomBuilds[1].BuildDate.replace('T', ' ');
            aux = aux.split('.');
            aux = aux[0];

            $('.bottombuild2 .topBuildHeader').html('<span class="large">' + data.BottomBuilds[1].Name + '</span>');
            $('.bottombuild2 .buildcoins').html('<h3 class="pull-right"><i class="glyphicon glyphicon-bitcoin glyphicon-s color-white"></i> ' + formatNum(data.BottomBuilds[1].Score) + '</h3>');
            $('.bottombuild2 .dt').html('<span class="mt10">' + aux + '</span>');
            aux = data.BottomBuilds[2].BuildDate.replace('T', ' ');
            aux = aux.split('.');
            aux = aux[0];
            $('.bottombuild3 .topBuildHeader').html('<span class="large">' + data.BottomBuilds[2].Name + '</span>');
            $('.bottombuild3 .buildcoins').html('<h3 class="pull-right"><i class="glyphicon glyphicon-bitcoin glyphicon-s color-white"></i> ' + formatNum(data.BottomBuilds[2].Score) + '</h3>');
            $('.bottombuild3 .dt').html('<span class="mt10">' + aux + '</span>');

            $('.userCodeReview1Name').html(data.TopCodeReviewers[0].Name);
            $('.userCodeReview2Name').html(data.TopCodeReviewers[1].Name);
            $('.userCodeReview3Name').html(data.TopCodeReviewers[2].Name);

            $('.userCodeReview3Img').attr("src", "/Content/Images/" + data.TopCodeReviewers[2].Name.replace(' ', '') + ".jpg");
            $('.userCodeReview2Img').attr("src", "/Content/Images/" + data.TopCodeReviewers[1].Name.replace(' ', '') + ".jpg");
            $('.userCodeReview1Img').attr("src", "/Content/Images/" + data.TopCodeReviewers[0].Name.replace(' ', '') + ".jpg");


            $('.userCodeReviewMonth1Img').attr("src", "/Content/Images/" + data.MonthTopCodeReviewers[0].Name.replace(' ', '') + ".jpg");
            $('.userCodeReviewMonth1Name').html(data.MonthTopCodeReviewers[0].Name);
            $('.userCodeReviewMonth1Total').html(data.MonthTopCodeReviewers[0].TotalReviews);

            $('.userCodeReviewMonth2Img').attr("src", "/Content/Images/" + data.MonthTopCodeReviewers[1].Name.replace(' ', '') + ".jpg");
            $('.userCodeReviewMonth2Name').html(data.MonthTopCodeReviewers[1].Name);
            $('.userCodeReviewMonth2Total').html(data.MonthTopCodeReviewers[1].TotalReviews);

            $('.userCodeReviewMonth3Img').attr("src", "/Content/Images/" + data.MonthTopCodeReviewers[2].Name.replace(' ', '') + ".jpg");
            $('.userCodeReviewMonth3Name').html(data.MonthTopCodeReviewers[2].Name);
            $('.userCodeReviewMonth3Total').html(data.MonthTopCodeReviewers[2].TotalReviews);


            $('.userCodeReview1Total').html(data.TopCodeReviewers[0].TotalReviews + ' Code Reviews');
            $('.userCodeReview2Total').html(data.TopCodeReviewers[1].TotalReviews + ' Code Reviews');
            $('.userCodeReview3Total').html(data.TopCodeReviewers[2].TotalReviews + ' Code Reviews');


            $('.userBadge1Name').html(data.TopBadgesOwners[0].Name);
            $('.userBadge2Name').html(data.TopBadgesOwners[1].Name);
            $('.userBadge3Name').html(data.TopBadgesOwners[2].Name);

            $('.userBadge3Img').attr("src", "/Content/Images/" + data.TopBadgesOwners[2].Name.replace(' ', '') + ".jpg");
            $('.userBadge2Img').attr("src", "/Content/Images/" + data.TopBadgesOwners[1].Name.replace(' ', '') + ".jpg");
            $('.userBadge1Img').attr("src", "/Content/Images/" + data.TopBadgesOwners[0].Name.replace(' ', '') + ".jpg");

            $('.userBadge1Total').html(data.TopBadgesOwners[0].TotalBadges + ' Total Badges');
            $('.userBadge2Total').html(data.TopBadgesOwners[1].TotalBadges + ' Total Badges');
            $('.userBadge3Total').html(data.TopBadgesOwners[2].TotalBadges + ' Total Badges');


            $('.topCheckins').text(data.RandomData[2].m_Item2);
            $('.topCheckinsNumber').text(data.RandomData[3].m_Item2);
            $('.totalCheckins').text(data.RandomData[1].m_Item2);


            $('.monthtop').text(data.MonthTopBuilds[0].Name);
            $('.monthtopDate').text(data.MonthTopBuilds[0].BuildDate);
            $('.scroreMonthTop').text(formatNum(data.MonthTopBuilds[0].Score));

            $('.monthbottom').text(data.MonthBottomBuilds[0].Name);
            $('.monthbottomDate').text(data.MonthBottomBuilds[0].BuildDate);
            $('.scroreMonthBottom').text(formatNum(data.MonthBottomBuilds[0].Score));

            $('.usersBadgesContainer').html('');

            for (var auxi = 0; auxi < data.AllUSersWithBadges.length; auxi++) {

                var stringHTML = '<div class="col-lg-12"><div class="user pull-left"><img src="/Content/Images/' + data.AllUSersWithBadges[auxi].Name.replace(' ', '') + '.jpg" alt="User Avatar" class="img-circle pull-left" width="160"><span class="large nameUser pull-left">' + data.AllUSersWithBadges[auxi].Name + '</span></div>';
                for (var auxi2 = 0; auxi2 < data.AllUSersWithBadges[auxi].Badges.length; auxi2++) {
                    stringHTML += '<div class="badgesUser"><div class="badgeAward ' + data.AllUSersWithBadges[auxi].Badges[auxi2].Code + '"><span class="glyphicon glyphicon-' + data.AllUSersWithBadges[auxi].Badges[auxi2].Code + '"></span><span class="badgeAwardLegend">' + data.AllUSersWithBadges[auxi].Badges[auxi2].Name + '</span></div></div>';

                }

                $('.usersBadgesContainer').append(stringHTML);

            }


            //var body = $("html, body");
            //var animationBody = 0;
            //var animationArray = [900, 1800, 2700, 3600, 4500, 5400, 6300, 7200, 8100, 9000, 9900, 10800, 11700, 12600, 13500, 14400, 15300, 16200, 17100, 18000, 0];
            //setInterval(function () {
            //    if (animationBody == animationArray.length) {
            //        animationBody = 0;
            //    }

            //    body.animate({ scrollTop: animationArray[animationBody] }, '500', 'swing', function () {

            //    });
            //    animationBody++;

            //}, 3);


        });


    }

    fetchData();
}
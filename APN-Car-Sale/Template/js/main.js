 AOS.init({
	duration: 800,
	easing: 'slide'
 });

(function($) {

	"use strict";

	var isMobile = {
		Android: function() {
			return navigator.userAgent.match(/Android/i);
		},
			BlackBerry: function() {
			return navigator.userAgent.match(/BlackBerry/i);
		},
			iOS: function() {
			return navigator.userAgent.match(/iPhone|iPad|iPod/i);
		},
			Opera: function() {
			return navigator.userAgent.match(/Opera Mini/i);
		},
			Windows: function() {
			return navigator.userAgent.match(/IEMobile/i);
		},
			any: function() {
			return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
		}
	};


  //  $(window).stellar({
  //  responsive: true,
  //  parallaxBackgrounds: true,
  //  parallaxElements: true,
  //  horizontalScrolling: false,
  //  hideDistantElements: false,
  //  scrollProperty: 'scroll'
  //});


	var fullHeight = function() {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	// loader
	var loader = function() {
		setTimeout(function() { 
			if($('#ftco-loader').length > 0) {
				$('#ftco-loader').removeClass('show');
			}
		}, 1);
	};
	loader();

	// Scrollax
   $.Scrollax();

	var carousel = function() {
		$('.carousel-testimony').owlCarousel({
			center: true,
			loop: true,
			items:1,
			margin: 30,
			stagePadding: 0,
			nav: false,
			navText: ['<span class="ion-ios-arrow-back">', '<span class="ion-ios-arrow-forward">'],
			responsive:{
				0:{
					items: 1
				},
				600:{
					items: 2
				},
				1000:{
					items: 3
				}
			}
		});

	};
	carousel();

	$('nav .dropdown').hover(function(){
		var $this = $(this);
		// 	 timer;
		// clearTimeout(timer);
		$this.addClass('show');
		$this.find('> a').attr('aria-expanded', true);
		// $this.find('.dropdown-menu').addClass('animated-fast fadeInUp show');
		$this.find('.dropdown-menu').addClass('show');
	}, function(){
		var $this = $(this);
			// timer;
		// timer = setTimeout(function(){
			$this.removeClass('show');
			$this.find('> a').attr('aria-expanded', false);
			// $this.find('.dropdown-menu').removeClass('animated-fast fadeInUp show');
			$this.find('.dropdown-menu').removeClass('show');
		// }, 100);
	});


	$('#dropdown04').on('show.bs.dropdown', function () {
	  console.log('show');
	});

	// scroll
	var scrollWindow = function() {
		$(window).scroll(function(){
			var $w = $(this),
					st = $w.scrollTop(),
					navbar = $('.ftco_navbar'),
					sd = $('.js-scroll-wrap');

			if (st > 150) {
				if ( !navbar.hasClass('scrolled') ) {
					navbar.addClass('scrolled');	
				}
			} 
			if (st < 150) {
				if ( navbar.hasClass('scrolled') ) {
					navbar.removeClass('scrolled sleep');
				}
			} 
			if ( st > 350 ) {
				if ( !navbar.hasClass('awake') ) {
					navbar.addClass('awake');	
				}
				
				if(sd.length > 0) {
					sd.addClass('sleep');
				}
			}
			if ( st < 350 ) {
				if ( navbar.hasClass('awake') ) {
					navbar.removeClass('awake');
					navbar.addClass('sleep');
				}
				if(sd.length > 0) {
					sd.removeClass('sleep');
				}
			}
		});
	};
	scrollWindow();

	var isMobile = {
		Android: function() {
			return navigator.userAgent.match(/Android/i);
		},
			BlackBerry: function() {
			return navigator.userAgent.match(/BlackBerry/i);
		},
			iOS: function() {
			return navigator.userAgent.match(/iPhone|iPad|iPod/i);
		},
			Opera: function() {
			return navigator.userAgent.match(/Opera Mini/i);
		},
			Windows: function() {
			return navigator.userAgent.match(/IEMobile/i);
		},
			any: function() {
			return (isMobile.Android() || isMobile.BlackBerry() || isMobile.iOS() || isMobile.Opera() || isMobile.Windows());
		}
	};

	var counter = function() {
		
		$('#section-counter, .hero-wrap, .ftco-counter').waypoint( function( direction ) {

			if( direction === 'down' && !$(this.element).hasClass('ftco-animated') ) {

				var comma_separator_number_step = $.animateNumber.numberStepFactories.separator(',')
				$('.number').each(function(){
					var $this = $(this),
						num = $this.data('number');
						console.log(num);
					$this.animateNumber(
					  {
						number: num,
						numberStep: comma_separator_number_step
					  }, 7000
					);
				});
				
			}

		} , { offset: '95%' } );

	}
	counter();


	var contentWayPoint = function() {
		var i = 0;
		$('.ftco-animate').waypoint( function( direction ) {

			if( direction === 'down' && !$(this.element).hasClass('ftco-animated') ) {
				
				i++;

				$(this.element).addClass('item-animate');
				setTimeout(function(){

					$('body .ftco-animate.item-animate').each(function(k){
						var el = $(this);
						setTimeout( function () {
							var effect = el.data('animate-effect');
							if ( effect === 'fadeIn') {
								el.addClass('fadeIn ftco-animated');
							} else if ( effect === 'fadeInLeft') {
								el.addClass('fadeInLeft ftco-animated');
							} else if ( effect === 'fadeInRight') {
								el.addClass('fadeInRight ftco-animated');
							} else {
								el.addClass('fadeInUp ftco-animated');
							}
							el.removeClass('item-animate');
						},  k * 50, 'easeInOutExpo' );
					});
					
				}, 100);
				
			}

		} , { offset: '95%' } );
	};
	contentWayPoint();


	// navigation
	var OnePageNav = function() {
		$(".smoothscroll[href^='#'], #ftco-nav ul li a[href^='#']").on('click', function(e) {
			e.preventDefault();

			var hash = this.hash,
					navToggler = $('.navbar-toggler');
			$('html, body').animate({
			scrollTop: $(hash).offset().top
		  }, 700, 'easeInOutExpo', function(){
			window.location.hash = hash;
		  });


		  if ( navToggler.is(':visible') ) {
			navToggler.click();
		  }
		});
		$('body').on('activate.bs.scrollspy', function () {
		  console.log('nice');
		})
	};
	OnePageNav();


	// magnific popup
	$('.image-popup').magnificPopup({
	type: 'image',
	closeOnContentClick: true,
	closeBtnInside: false,
	fixedContentPos: true,
	mainClass: 'mfp-no-margins mfp-with-zoom', // class to remove default margin from left and right side
	 gallery: {
	  enabled: true,
	  navigateByImgClick: true,
	  preload: [0,1] // Will preload 0 - before current, and 1 after the current image
	},
	image: {
	  verticalFit: true
	},
	zoom: {
	  enabled: true,
	  duration: 300 // don't foget to change the duration also in CSS
	}
  });

  $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
	disableOn: 700,
	type: 'iframe',
	mainClass: 'mfp-fade',
	removalDelay: 160,
	preloader: false,

	fixedContentPos: false
  });


	$('#book_pick_date,#book_off_date').datepicker({
	  'format': 'm/d/yyyy',
	  'autoclose': true
	});
	$('#time_pick').timepicker();





})(jQuery);



$(document).ready(function () {
	function Contains(text_one, text_two) {
		if (text_one.indexOf(text_two) != -1)
			return true;
	}

	$("#searchItem").keyup(function () {
		var searchText = $("#searchItem").val().toLowerCase();
		$(".searchItem").each(function () {
			if (!Contains($(this).text().toLowerCase(), searchText)) {
				$(this).hide();
			}
			else {
				$(this).show();
			}
		});
	});
});

//var x = document.getElementById("demo");

function getLocation() {
	if (navigator.geolocation) {
		navigator.geolocation.getCurrentPosition(showPosition);
	} else {
		alert("Geolocation is not supported by this browser.");
	}
}

function showPosition(position) {

	alert("Latitude: " + position.coords.latitude + "<br>Longitude: " + position.coords.longitude);

}




    // load subcategory by category id
    function loadSubCategory(id) {
        var url = "/Ads/GetSubCategoryByCategoryId?cid=" + id;
        var setData = $("#id-" + id);
        $.ajax({
            type: "GET",
            url: url,
            success: function (data) {
                var obj = JSON.parse(JSON.stringify(data));
                setData.html(" ");
                for (var i = 0; i < obj.length; i++) {
                    var data = "<li href='#' onclick='load(" + obj[i].SId + ")'><a href='#'  style='color:#fff'>" + obj[i].SName + "</a></li>";

                    //$("#id-"+obj[i].SId).click(load(0, obj[i].SId));
                    setData.append(data);

                }

            }
        })
    }



$(function () { // Submit pageSizeForm when another pageSize value is selected
	$("#pageSize").change(function () {
		$("#pageSizeForm").submit();
	});
});

// load category
$.get("/Admin/GetCategory", null, DataBind);
function DataBind(List) {
	//This Code For Receive All Data From Controller And Show It In Client Side
	var SetData = $("#category");
	for (var i = 0; i < List.length; i++) {

	    var data = "<li>" +
					"<a href='#' onclick='load(null, null, null, "+List[i].id+")' id=" + List[i].id + " data-toggle='collapse' data-target=#id-" + List[i].id + " >" + List[i].name + "<span id='subcount'></span></a>" +                    
                    "<ul id=id-" + List[i].id + " class='list right_sliding' style='background-color: #3770a9;'>" +
					"</ul>"
		            "</li>";
		SetData.append(data);
		loadSubCategory(List[i].id)

	}
}



                    

//function load pagination
function load(sid, txtSearch, page, cid) {
	//alert(cid);
	$.ajax({
		url: "/Ads/getAllads",
		type: "GET",
		data: { txtSearch: txtSearch, page: page, sid: sid , cid: cid},
		dataType: 'json',
		contentType: 'application/json;charset=utf-8',
		success: function (result) {
			var str = "";
			var numSize = "";
			$("#load-pagination").html(" ");
			$.each(result.data, function (index, value) {

				str += "<div class='col-md-3 col-sm-6 col-xs-6 searchItem'>";
				str += "<div class='product-grid4'>";
				str += "<div class='product-image4' >";

				//str += "</div>";
				//str += "<div class='text' style='padding:8px'>";
				//str += "<p><a href='/Ads/ad'> dfdfdfdfd d fdfdfdf" + value.Brand + "</a></p>";
				//str += "<h4> " + value.Model + "</h4>";
				str += "<a href='/Ads/ad'>";
				str += "<img class='pic-1' src='/Template/images/car-1.jpg'>";
				str += "<img class='pic-2' src='/Template/images/car-11.jpg'>";
			    str += "</a>";

			    str += "<div class='product-content'>";
			    str += "<h3 class='title'><a href='/Ads/ad'>" + value.Brand + "</a></h3>";
			    str += "<h3 class='title'><a href='/Ads/ad'>" + value.Model + "</a></h3>";
			    str += "<div class='price'>";
			    str += "$14.40";
			    str += "<span>$16.00</span>";
			    str += "</div>";
			    str += "</div>";

				//str += "<span style='color:#adb2da'>Address</span>";

				//str += "<div class='meta'>";
				//str += "<div style='font-size: 12px;'><a href='#'><span class='icon-calendar'></span> July 12, 2018</a></div>";
				//str += "</div>";
				str += "</div>";
				str += "</div>";
				str += "</div>";

			    //str += "<div class='col-md-3 col-sm-6 searchItem'>";
				//str += "<div class='product-grid4'>";
				//str += "<div class='product-image4'>";

				//str += "<a href='#'>";
				//str += "<img class='pic-1' src='http://bestjquery.com/tutorial/product-grid/demo5/images/img-1.jpg'>";
				//str += "<img class='pic-2' src='http://bestjquery.com/tutorial/product-grid/demo5/images/img-2.jpg'>";
				//str += "</a>";

				//str += "<span class='product-new-label'>New</span>";
				//str += "<span class='product-discount-label'>-10%</span>";
				//str += "<div class='product-content'>";
				//str += "<h3 class='title'><a href='#'>"+ value.Model +"</a></h3>";
				//str += "<div class='price'>";
				//str += "$14.40";
				//str += "<span>$16.00</span>";
				//str += "</div>";
				//str += "</div>";
				//str += "</div>";
				//str += "</div>";
			    // style='font-size:1vw;'
				//create pagination
				var pagination_string = "";
				var pageCurrent = result.pageCurrent;
				numSize = result.numSize;
				if (numSize >= 1) {

					//create button previous
					if (pageCurrent > 1) {
						var pagePrevious = pageCurrent - 1;
						pagination_string += '<li class="page-item"><a href="" class="page-link" data-page=' + pagePrevious + '><<</a></li>';
					}

					for (i = 1; i <= numSize; i++) {
						if (i == pageCurrent) {
							pagination_string += '<li class="page-item active"><a href="" class="page-link" data-page=' + i + '>' + pageCurrent + '</a></li>';
						} else {
							pagination_string += '<li class="page-item"><a href="" class="page-link" data-page=' + i + '>' + i + '</a></li>';
						}
					}

					//create button next
					if (pageCurrent > 0 && pageCurrent < numSize) {
						var pageNext = pageCurrent + 1;
						pagination_string += '<li class="page-item"><a href="" class="page-link"  data-page=' + pageNext + '>>></a></li>';
					}

					//load pagination
					$("#load-pagination").html(pagination_string);
				}
			});

			//load str to class="load-list"
			$(".load-list").html(str);
		}
	});
}


//click event pagination
$("body").on("click", ".pagination li a", function (event) {
	event.preventDefault();
	var page = $(this).attr('data-page');

	//load event pagination
	var txtSearch = $(".txtSearch").val();
	if (txtSearch != "") {
		load(txtSearch, null, page, null)
	}
	else {
		load(null, null, page, null);
	}

});




$(document).ready(function () {

	//click event search
	$("#search").click(function () {

		var txtSearch = $(".txtSearch").val();
		if (txtSearch != "") {
			load(null, txtSearch, 1, null)
		}
		else {
			load(null, null, 1, null);
		}

	});
	//load init
	load(null, null, 1, null);
});



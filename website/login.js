$('document').ready(function(){

	//make login box slide in from right
	$('.body-right').css("transform", "translateX(0)");
	$('.body-container').css("backgroundColor", "rgba(10, 10, 10, 0.7)");

	$('.signup-container').css('transform', 'scale(0)');
	$('.signup-container').css('height', '0');
	$('.login-container').css('transform', 'scale(1)');
	$('.login-container').css('height', '100%');

	//toggle between login and sign up
	$('.signup-link').on('click', function(){
		$('.login-container').css('height', '0');
		$('.signup-container').css('transform', 'scale(1)');
		$('.signup-container').css('height', '100%');
		$('.login-container').css('transform', 'scale(0)');
		
	});

	$('.login-link').on('click', function(){
		$('.signup-container').css('transform', 'scale(0)');
		$('.signup-container').css('height', '0');
		$('.login-container').css('transform', 'scale(1)');
		$('.login-container').css('height', '100%');
	});
});
window.alert = function() {};

$('document').ready(function(){


	console.log("DOCUMENT READY");

	$('canvas').attr('tabindex', 1);

	//sign user out when sign out button is clicked
	$('#sign-out').on('click', function(){
		firebase.auth().signOut().then(function() {
		// Sign-out successful.
		}).catch(function(error) {
		  // An error happened.
		  console.log(error);
		});
	});

	//redirect to login page if user is not signed in
	firebase.auth().onAuthStateChanged(function(user) {
	  if (!user) {
	  	window.location = "../index.html";
	  }
	  else{
	  	$('#curUser').html(firebase.auth().currentUser.displayName);
	  	if(firebase.auth().currentUser.photoURL){
	  	$('#open-profile').after('<div class = "image-cropper"><img src ="' + firebase.auth().currentUser.photoURL + '" alt = "profile picture" /></div>');
	  	$('#open-profile').remove();
	  	$('#profile img').attr('id', 'open-profile');
	  	}
	  	$('#open-profile').on('click', function(){
				$('.userprofile').css('display', 'block');
				$('.userprofile-sidebar').css('width', '400px');
				$('.profile-email').html(firebase.auth().currentUser.email);
			  	$('.profile-username').html(firebase.auth().currentUser.displayName);
			  	if(firebase.auth().currentUser.photoURL)
			  		$('.profile-image').html('<div class = "image-cropper"><img src ="' + firebase.auth().currentUser.photoURL + '" alt = "profile picture" /></div>');
		});
	  }
	});

	$('#open-profile').on('click', function(){
		$('.userprofile').css('display', 'block');
		$('.userprofile-sidebar').css('width', '400px');
		$('.profile-email').html(firebase.auth().currentUser.email);
	  	$('.profile-username').html(firebase.auth().currentUser.displayName);
	  	if(firebase.auth().currentUser.photoURL)
	  		$('.profile-image').html('<img src ="' + firebase.auth().currentUser.photoURL + '" alt = "profile picture" />');
	});

	$('.userprofile').on('click', function(e){
		if (e.target.className == 'userprofile')
		{
			$('.userprofile').css('display', 'none');
			$('.userprofile-sidebar').css('width', '0');
		}
	});


	var database = firebase.firestore();

	// var gameDatabase = firebase.database();

	// trying to read from high scores realtime database

	// function updateScores(postElement, value){
	// 	console.log(value);
	// }

	// var highscores = firebase.database().ref('/users/');
	// highscores.on('value', function(snapshot) {
	// 	console.log("READING FROM DATABASE");
	//   updateScores(postElement, snapshot.val());
	// });

	$('#scoreboard-button').on('click', function(){
		console.log('clicked');
		$('#scoreboard-modal').modal('show');
	});

	$('#help-button').on('click', function(){
		console.log('clicked');
		$('#help-modal').modal('show');
	});

	var database2 = firebase.database();
	var scoreboardJSON;

	let scoreboardARRAY = [];

	var database2ref = firebase.database().ref('/users/');
	database2ref.on('value', function(snapshot) {
		console.log("DATABASE 2 VALUES !!!!");
		scoreboardJSON = snapshot.val();
	 	console.log(scoreboardJSON);
	 	let scoreIndex = 1;

	 	for (var key in scoreboardJSON) {
		  if (scoreboardJSON.hasOwnProperty(key)) {

		    console.log(key + ": " + scoreboardJSON[key].userName + scoreboardJSON[key].finalTime);
		    scoreboardARRAY.push(scoreboardJSON[key]);

		    // $('table tbody').append('<tr> <td>'+scoreboardJSON[key].finalTime+'</td> <td>'+scoreboardJSON[key].userName+'</td> </tr>');
		    scoreIndex++;
		  }
		}

		for (let j = 0; j<scoreboardARRAY.length;j++){
		    $('table tbody').append('<tr> <td>'+scoreboardARRAY[j].finalTime+'</td> <td>'+scoreboardARRAY[j].userName+'</td> </tr>');

		}
			console.log('ARRAY');
		    console.log(scoreboardARRAY);
	});


	var MessagesdocRef = database.collection("messages");
	//Live updates and Message retrieval
    MessagesdocRef.orderBy("date", "asc")
        .onSnapshot(function(snapshot) {
        snapshot.docChanges().forEach(function(change) {
          var data = change.doc.data();
          var user = data.user;
          var photoURL = data.photoURL;
          var date;
          if (data.date != null)
          	date = data.date.toDate();
          else
          	date = new Date();
          var message = data.message;
          var datearray = date.toString().split(' ');
          //FORMATTING
          var formatted_message;
          if (photoURL){
	          formatted_message = '<div><div class = "image-cropper"><img src = '+ photoURL+'></div><strong>' + user + ':  ' + '</strong>'
	          + message + '</br>' + '<span class = "live-feed-time">Sent on ' + datearray[0] + ' ' + datearray[1] + ' ' + datearray[2] + ' at ' + datearray[4] +'</span>' + '</div>';
      	  }
      	  else{
      	  	formatted_message = '<div><div><i class="fas fa-user-circle"></i></div><strong>' + user + ':  ' + '</strong>'
	          + message + '</br>' + '<span class = "live-feed-time">Sent on ' + datearray[0] + ' ' + datearray[1] + ' ' + datearray[2] + ' at ' + datearray[4] + '</span>' + '</div>';
      	  }
          //APPEND TO HTML
          $('.live-feed').append(formatted_message);
        });
    });

});

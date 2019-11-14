// /public/javascript.js

//TODO: add currentUser functionality :LINE 77
//TODO: Implement security
// function addChut(user_id) {
//     var log= document.createElement('div');
//     log.className= 'log';
//     var textarea= document.createElement('textarea');
//     var input= document.createElement('input');
//     input.value= user_id;
//     input.readonly= True;
//     var button= document.createElement('input');
//     button.type= 'button';
//     button.value= 'Message';
//
//     var chut= document.createElement('div');
//     chut.className= 'chut';
//     chut.appendChild(log);
//     chut.appendChild(textarea);
//     chut.appendChild(input);
//     chut.appendChild(button);
//     document.getElementById('chuts').appendChild(chut);
//
//     button.onclick= function() {
//         alert('Send '+textarea.value+' to '+user_id);
//     };
//
//     return chut;
// }

//https://stackoverflow.com/questions/2794137/sanitizing-user-input-before-adding-it-to-the-dom-in-javascript

//Sanitizing user input
function isAlphaNumeric(str) {
  var code, i, len;

  for (i = 0, len = str.length; i < len; i++) {
    code = str.charCodeAt(i);
    if (!(code > 47 && code < 58) && // numeric (0-9)
        !(code > 64 && code < 91) && // upper alpha (A-Z)
        !(code > 96 && code < 123) &&  // lower alpha (a-z)
        !(code == 32 || code == 33 || code == 44 || code == 46 || code == 63))// space, exclamation point, comma, period,  and question mark
        {return false;}
  }
  return true;
};

// When we receive a message
// it will be like { user: 'username', message: 'text' }
// socket.on('message', function (data) {
//   $('.chat').append('<p><strong>' + data.user + '</strong>: ' + data.message + '</br>' + '<sup>Sent on ' + data.date + '</sup>' + '</p>');
// });


//To retrieve date
function getDate(){
  var today = new Date().toLocaleString();
  return today;
}


var database = firebase.firestore();
var docRef = database.collection("messages");

// When the form is submitted
$('form').submit(function (e) {
    // Avoid submitting it through HTTP
    e.preventDefault();
    // Retrieve the message from the user
    var message = $(e.target).find('input').val();
    //Don't take unsafe inputs
    if(!isAlphaNumeric(message)){
      alert("Please use only Alpha-numeric characters and . , ! ?")
    }else{
      //Send the message to the server
      docRef.add({
        "user": "me",
        //"user": firebase.auth().currentUser.email(), //maybe change to username
        "message": message,
        "date": getDate()
      })
      .then(function(docRef){
    			//console.log("document written with ID: " + docRef.id);
    	})
    	.catch(function(error){
    			console.log(error);
    	});
    }
  // Clear the input and focus it for a new message
  e.target.reset();
  $(e.target).find('input').focus();
});


//Retrieve messages
// docRef.get().then(function(querySnapshot) {
//     querySnapshot.forEach(function(doc) {
//         //GET DATA
//         //console.log(doc.data())
//         var data = doc.data();
//         var user = data.user;
//         var date = data.date;
//         var message = data.message;
//         //FORMATTING
//         var formatted_message = '<p><strong>' + user + '</strong>: '
//         + message + '</br>' + '<sup>Sent on ' + date + '</sup>' + '</p>';
//
//         //APPEND TO HTML
//         $('.chat').append(formatted_message);
//       });
// });


//Live updates and Message retrieval
docRef
    .onSnapshot(function(snapshot) {
    snapshot.docChanges().forEach(function(change) {
      var data = change.doc.data();
      var user = data.user;
      var date = data.date;
      var message = data.message;
      //FORMATTING
      var formatted_message = '<p><strong>' + user + '</strong>: '
      + message + '</br>' + '<sup>Sent on ' + date + '</sup>' + '</p>';
      //APPEND TO HTML
      $('.chat').append(formatted_message);
    });
});
// //GET DATA
// console.log(doc.data())
// var data = doc.data();
// var user = data.user;
// var date = data.date;
// var message = data.message;
// //FORMATTING
// var formatted_message = '<p><strong>' + user + '</strong>: '
// + message + '</br>' + '<sup>Sent on ' + data + '</sup>' + '</p>';
//
// //APPEND TO HTML
// $('.chat').append(formatted_message);
// });

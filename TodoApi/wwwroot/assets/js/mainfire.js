// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { getMessaging, getToken, onMessage } from "firebase/messaging";

// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: "AIzaSyCBySLL52e7dOS8dhllM9YcgOsDR-8Y2ho",
  authDomain: "almazr3a-df3f4.firebaseapp.com",
  projectId: "almazr3a-df3f4",
  storageBucket: "almazr3a-df3f4.appspot.com",
  messagingSenderId: "204836888665",
  appId: "1:204836888665:web:6b408918ccdea3302f1148",
  measurementId: "G-V12WKBV60S"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
const analytics = getAnalytics(app);


Notification.requestPermission().then(permission => {
    if (permission === 'granted') {
      console.log('Notification permission granted.');
    } else {
      console.log('Unable to get permission to notify.');
    }
  });



  getToken(messaging, { vapidKey: 'your-public-vapid-key' }).then((currentToken) => {
    if (currentToken) {
      console.log('FCM Token:', currentToken);
      // Send the token to your server and save it to send notifications later
    } else {
      console.log('No registration token available. Request permission to generate one.');
    }
  }).catch((err) => {
    console.log('An error occurred while retrieving token. ', err);
  });
  
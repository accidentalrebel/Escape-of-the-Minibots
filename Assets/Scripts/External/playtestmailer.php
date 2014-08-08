<?php
	require 'phpmailer/PHPMailerAutoload.php';

    $email = new PHPMailer();
	$email->From      = 'karlo@accidentalrebel.com';
	$email->FromName  = 'PlayTestMailer';
	$email->Subject   = $_GET['emailSubject'];
	$email->Body      = $_GET["emailTxt"];
	$email->AddAddress( 'accidentalrebel_3avg@sendtodropbox.com' );
	
	$string 	= $_GET['fileData'];
	$filename 	= $_GET['fileName'];
	$encoding	= 'base64';
	$type 		= 'text/plain';
	
	$email->AddStringAttachment($string,$filename,$encoding,$type);
	
	$email->Send();
	
	// http://www.accidentalrebel.com/game-files/minibots/playtestmailer.php?
	// emailSubject=testSubject&emailTxt=sampleEmailText&fileData=sampleFileData&fileName=sampleFileName.txt
?>
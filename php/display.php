<?php
    // Send variables for the MySQL database class.
	$db = new PDO('mysql:host=localhost;dbname=kittydash;charset=utf8', 'root', 'kittydash');
    
	$query = "SELECT * FROM `scores` ORDER BY `score` DESC LIMIT 10";
	$data = $db->query($query);
	$result = $data->fetchAll(PDO::FETCH_ASSOC);
 
    foreach($result as $row)
    {
         echo $row['name'] . "\t" . $row['score'] . "\n";
    }
?>
<?php
if (!file_exists('uploads/')) {
    mkdir('uploads/', 0777, true);
}
$target_dir = "uploads/";


if (isset($_POST["submit"])) {
    //check if 'document' key exists in $_FILES array
    if (!isset($_FILES["document"]) || $_FILES["document"]["error"] == UPLOAD_ERR_NO_FILE) {
        echo '<p class = "no-file-upload" >No file was uploaded. Please select a file</p>';
        exit;

    }//now the warnings are gone

    //6. sanitizing file name
    $filename = basename($_FILES["document"]["name"]);
    $filename = preg_replace('/[^a-zA-Z0-9_\.-]/', '_', $filename);

    //5. generating new file name to prevent overwriting
    $target_file = $target_dir . uniqid() . "_" . $filename; //uniquid() generates new unique id based on microtime

    //1. PDF file type validation
    $fileType = strtolower(pathinfo($target_file, PATHINFO_EXTENSION));
    if ($_FILES["document"] and $fileType != "pdf") {
        echo '<p class = "not-pdf">Sorry! Choose a pdf file.</p>';
        exit;
    }

    //2. Validating the file size 
    if ($_FILES["document"]["size"] > 5242880) { //pdf <25 MB allowed
        echo '<p class = "sizeok">Too large file. Limit to 5MB.</p>';
        exit;
    }

    // Move uploaded file
    if (move_uploaded_file($_FILES["document"]["tmp_name"], $target_file)) {
        echo '<p class = "success">Thank you. Your file is uploaded.</p>';
    } else {
        echo '<p class = "error" >Sorry, there was an error uploading your file.</p>';
    }
}
?>
<form method="POST" enctype="multipart/form-data">
    Select document to upload (only pdf are allowed): <input type="file" name="document">
    <input type="submit" value="Upload Document" name="submit">
</form>
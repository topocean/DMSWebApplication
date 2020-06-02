<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="WebApplication1._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>File Upload</title>
    <link href="Style.css" rel="Stylesheet" />

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.1.min.js"></script>

    <script type="text/javascript">
        var maxRequestLength = 4194304;
    </script>

    <script>
        var dropZone;
        var dropZone1;
        var uploadedFiles = 0;
        var filesDropped;

        // Initializes the dropZone
        $(document).ready(function () {
            dropZone = $('#dropZone');
            dropZone1 = $('#dropZone1');
            dropZone.removeClass('error');
            dropZone1.removeClass('error');

            // Check if window.FileReader exists to make 
            // sure the browser supports file uploads
            if (typeof(window.FileReader) == 'undefined') {
                dropZone.text('Browser Not Supported!');
                dropZone1.text('Browser Not Supported!');
                dropZone.addClass('error');
                dropZone1.addClass('error');
                return;
            }

            // Add a nice drag effect
            dropZone[0].ondragover = function () {
                dropZone.addClass('hover');
                return false;
            };

            dropZone1[0].ondragover = function () {
                dropZone1.addClass('hover');
                return false;
            };

            // Remove the drag effect when stopping our drag
            dropZone[0].ondragend = function () {
                dropZone.removeClass('hover');
                return false;
            };

            dropZone1[0].ondragend = function () {
                dropZone1.removeClass('hover');
                return false;
            };

            // The drop event handles the file sending
            dropZone[0].ondrop = function(event) {
                // Stop the browser from opening the file in the window
                event.preventDefault();

                // Get the file and the file reader
                var files = event.dataTransfer.files;

                // Assign how many files were dropped to a variable
                filesDropped = files.length;

                // Empty the progressZone div
                $('#progressZone').empty();

                // Loop through dropped files
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];

                    // Add a div for each file being uploaded
                    $('#progressZone').append('<div id="progress' + i + '"></div>');

                    // Validate file size
                    if(file.size > maxRequestLength) {
                        $('#progress' + i).html(' \
                            <div class="fileName">' + trimString(file.name) + '</div> \
                            <div class="barError"></div> \
                            <div class="uploadError">Error: File Too Large!</div>');
                        uploadedFiles++;
                        continue;
                    }

                    // Report upload in progress
                    dropZone.text('Upload In Progress...');

                    // Send the file
                    uploadFile(file, i);
                }
            };

            // The drop event handles the file sending
            dropZone1[0].ondrop = function(event) {
                // Stop the browser from opening the file in the window
                event.preventDefault();

                // Get the file and the file reader
                var files = event.dataTransfer.files;

                // Assign how many files were dropped to a variable
                filesDropped = files.length;

                // Empty the progressZone div
                $('#progressZone1').empty();

                // Loop through dropped files
                for (var i = 0; i < files.length; i++) {
                    var file = files[i];

                    // Add a div for each file being uploaded
                    $('#progressZone1').append('<div id="progress1' + i + '"></div>');

                    // Validate file size
                    if(file.size > maxRequestLength) {
                        $('#progress1' + i).html(' \
                            <div class="fileName">' + trimString(file.name) + '</div> \
                            <div class="barError"></div> \
                            <div class="uploadError">Error: File Too Large!</div>');
                        uploadedFiles++;
                        continue;
                    }

                    // Report upload in progress
                    dropZone1.text('Upload In Progress...');

                    // Send the file
                    uploadFile1(file, i);
                }
            };

        });

        // Upload the file
        function uploadFile(file, i) {
            var xhr = new XMLHttpRequest();
            //xhr.upload.addEventListener('progress', uploadProgress, false);
            xhr.upload.addEventListener('progress', function (event) {
                $('#progress' + i).html(' \
                <div class="fileName"> ' + trimString(file.name) + '</div> \
                <div class="barHolder"> \
                    <div id="barFiller' + i + '" class="barFiller" style="width: '+ parseInt(event.loaded / event.total * 100) +'%"></div> \
                </div> \
                <div id="uploadProgress' + i + '" class="uploadProgress">Uploading: ' + parseInt(event.loaded / event.total * 100) + '%</div>');
            }, false);
            xhr.onreadystatechange = function (event) {
                return function() {
                    if (event.target.readyState == 4) {
                        if (event.target.status == 200 || event.target.status == 304) {
                            $('#uploadProgress' + i).html('Upload Complete.');
                            uploadedFiles++;
                            
                        }
                        else {
                            $('#barFiller' + i).addClass('barError');
                            $('#uploadProgress' + i).html('Error: XMLHttpRequest Error!');
                            $('#uploadProgress' + i).addClass('uploadError');
                            uploadedFiles++;
                        }
                        // If all files have been uploaded show drop files here text
                        if (uploadedFiles >= filesDropped) {
                            dropZone.removeClass('hover');
                            dropZone.text('Drop Files Here.');
                        }
                    }
                }(event);
            };
            xhr.open('POST', '/Default.aspx?UploadRegion=0', true);
            xhr.setRequestHeader('X-FILE-NAME', file.name);
            xhr.send(file);
        }

        // Upload the file
        function uploadFile1(file, i) {
            var xhr = new XMLHttpRequest();
            //xhr.upload.addEventListener('progress', uploadProgress, false);
            xhr.upload.addEventListener('progress', function (event) {
                $('#progress1' + i).html(' \
                <div class="fileName"> ' + trimString(file.name) + '</div> \
                <div class="barHolder"> \
                    <div id="barFiller' + i + '" class="barFiller" style="width: '+ parseInt(event.loaded / event.total * 100) +'%"></div> \
                </div> \
                <div id="uploadProgress1' + i + '" class="uploadProgress">Uploading: ' + parseInt(event.loaded / event.total * 100) + '%</div>');
            }, false);
            xhr.onreadystatechange = function (event) {
                return function() {
                    if (event.target.readyState == 4) {
                        if (event.target.status == 200 || event.target.status == 304) {
                            $('#uploadProgress1' + i).html('Upload Complete.');
                            uploadedFiles++;
                            
                        }
                        else {
                            $('#barFiller' + i).addClass('barError');
                            $('#uploadProgress1' + i).html('Error: XMLHttpRequest Error!');
                            $('#uploadProgress1' + i).addClass('uploadError');
                            uploadedFiles++;
                        }
                        // If all files have been uploaded show drop files here text
                        if (uploadedFiles >= filesDropped) {
                            dropZone1.removeClass('hover');
                            dropZone1.text('Drop Files Here.');
                        }
                    }
                }(event);
            };
            xhr.open('POST', '/Default.aspx?UploadRegion=1', true);
            xhr.setRequestHeader('X-FILE-NAME', file.name);
            xhr.send(file);
        }

        function trimString(sentSting) {
            allowedLength = 27;
            var newString;
            if (sentSting.length > allowedLength) {
                newString = sentSting.substring(0, allowedLength) + '...';
            } else {
                newString = sentSting;
            }
            return newString;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="dropZone" >
            Drop Files Here.
        </div>
        <div id="progressZone"></div>
        <div id="dropZone1" >
            Drop Files Here 1.
        </div>
        <div id="progressZone1"></div>
    </form>
</body>
</html>
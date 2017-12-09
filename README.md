# Simple files microservice

This service provides Upload/Preload/Download files functions as a service

## API

### `GET api/files/{uid}`

Returns file metadata

### `POST api/files/upload`

Uploads array of files to permanent file storage

body: 
```
files[]
```

### `POST api/files/preload`

Uploads array of files to temporary file storage

body: 
```
files[]
```

### `GET api/files/{uid}/download`

Downloads file


### `PATCH api/files/{uid}` 

Update file metadata (filename)

### `DELETE api/files/{uid}/delete` 

Softly deletes file (move to trash)


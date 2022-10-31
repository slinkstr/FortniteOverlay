# FortniteOverlay

Gear overlay for Fortnite. Automatically gets current squad as you play and overlays a screenshot of their gear in the top-left corner.

Requires everyone to be running it to get the screenshots.

# Config

Requires config.json next to the executable. An example is below.

```
{
    "UploadEndpoint": "http://example.com/fortnitegear/upload.php",
    "SecretKey": "SECRET_KEY",
    "ImageLocation": "http://example.com/fortnitegear/images"
}
```

# Server setup

I am using [ShareX-Custom-Upload](https://github.com/Inteliboi/ShareX-Custom-Upload) slightly tweaked, included as upload.php.

Make sure to change the secret key.

Below is a snippet from my NGINX config to enable auto-indexing. (The json format is very handy.)

```
location /fortnitegear {
    location ~ \.php$ {
        include snippets/fastcgi-php.conf;
        fastcgi_pass unix:/run/php/php7.4-fpm.sock;
    }

    location ~ /images {
        autoindex on;
        autoindex_format json;
    }
}
```
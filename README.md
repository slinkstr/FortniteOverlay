# FortniteOverlay

![Preview screenshot](PreviewScreenshot.jpg)

Gear overlay for Fortnite. Automatically gets current squad as you play and overlays a screenshot of their gear in the top-left corner if they're also running it.

Designed for 100% HUD scale at 1080p/1440p. Other resolutions and scales are calculated from these and may not be accurate.

Uses ~5.5MB/hour per person down and ~5.5MB/hour up. Doesn't upload or download unless Fortnite is open and you're in a party with other players.

# Config

Requires config.json next to the executable. An example is below.

```
{
    "UploadEndpoint": "http://example.com/fortnitegear/upload.php",
    "SecretKey": "SECRET_KEY",
    "ImageLocation": "http://example.com/fortnitegear/images",
    "HUDScale": 100
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
# oggstreamer
Ogg transcoding+streaming ASP.NET Core server spike.

This is a Proof-of-Concept that we can build an ASP.NET Core 2.0+ server that can turn FLAC files to OGG/OGA files, and return the files in reasonable time.
- Caching the files is currently not considered. (Could just use an nginx container to do that OOB.)
- ffmpeg is used for encoding.
- No logging/monitoring.
- This is a spike, so tests aren't extensive.
- OGGs/OGAs are inherently streamable, therefore there's no need to implement range requests for now.

The app always looks for a file called test01.flac in the Oggstreamer/assets folder (ahem... directory), so that needs to be placed there in order to do testing. After doing this, `make run_server` runs the app, `make test` runs tests. (Both uses Docker.)

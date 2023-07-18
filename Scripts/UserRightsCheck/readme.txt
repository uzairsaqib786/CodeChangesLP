Deutsche Übersetzung folgt unten!

US:

The ll19webcheck analysing tool
========================================

The 'll19webcheck.exe" application checks the ASPNET user's rights for your web. This helps you to quickly find out if there are any rights missing. The application also checks for printer access for the ASPNET user. As List & Label requires a printer driver, this is a prerequisite for running a web application with List & Label. In order to check your web application proceed as follows:

1. Copy the files "ll19webcheck.exe" and "llwebcheck.aspx" from the "llwebcheck" directory of your installation into your web application's directory

2. Copy the "llwebcheck.dll" into your web's "\bin" directory (create one if necessary)

3. Start the "ll19webcheck.exe" and choose the URL of your web application if necessary

Make sure all rights are given appropriate (indicated by a green "Yes"). If necessary, assign rights to the ASPNET user. Also make sure a printer is available. The Microsoft Knowledgebase article Q184291 deals with this issue and describes how to install printers for the SYSTEM/ASPNET account. If you proceed as described there, you should be able to print from your web application.

http://support.microsoft.com/support/kb/articles/Q184/2/91.ASP

Hint: When starting the ll19webcheck.exe from a remote machine, please note the rights will be queried on the server, not the calling machine.


D:

Das Analysetool ll19webcheck
========================================

Die Anwendung 'll19webcheck.exe' prüft die Rechte des ASPNET Users ab, die für Ihr Webverzeichnis gesetzt wurden. Somit lässt sich schnell eine Übersicht schaffen, in welchem Verzeichnis eventuell Rechte fehlen. Desweiteren wird überprüft ob Drucker vorhanden sind und ob diese für den ASPNET User zugänglich sind. Ist dies nicht der Fall, so tritt bei Ihrer Webapplikation ein Fehler auf, da List & Label einen korrekt installierten Drucker benötigt. Damit Sie nun Ihre Webanwendung mit dem Tool prüfen lassen können, sind noch folgende Schritte notwendig:

1. Bitte kopieren Sie zunächst aus dem Verzeichnis "\llwebcheck" die Dateien "ll19webcheck.exe" und "llwebcheck.aspx" in Ihr Web-Hauptverzeichnis.

2. Anschliessend kopieren Sie bitte die Datei "llwebcheck.dll" in das Unterverzeichnis "\bin". Sollte dies nicht vorhanden sein, so erstellen Sie dieses.

3. Starten Sie nun die Anwendung "ll19webcheck.exe" und geben Sie ggf. die URL Ihrer Webanwendung an.

Stellt sich beim Ausführen der Anwendung heraus das der ASPNET User keine Rechte auf ein bestimmtes Verzeichnis hat, so müssen Sie diesem explizit diese Rechte zuweisen. In dem Fall, dass keine Drucker aufgelistet werden oder diese lediglich Rot dargestellt sind, müssen Sie ebenfalls die Rechte für die entsprechenden Drucker vergeben. Die Microsoft Knowledgebase hält unter der Nummer Q184291 einen englischsprachigen Artikel bereit, wie zur Einrichtung von Druckern für den SYSTEM beziehungsweise ASPNET Account vorzugehen ist. Sie erreichen den Artikel über den angegebenen Link. Nach erfolgreicher Übernahme der Konfiguration sollte der Druck innerhalb des Services möglich sein.

http://support.microsoft.com/support/kb/articles/Q184/2/91.ASP

Hinweis: Sollten Sie die Anwendung ll19webcheck.exe von einem separaten Rechner aus starten beachten Sie bitte, dass die Rechte / Informationen vom Webserver abgefragt werden und _nicht_ vom aufrufenden Rechner!
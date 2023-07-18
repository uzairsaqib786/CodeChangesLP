Deutsche �bersetzung folgt unten!

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

Die Anwendung 'll19webcheck.exe' pr�ft die Rechte des ASPNET Users ab, die f�r Ihr Webverzeichnis gesetzt wurden. Somit l�sst sich schnell eine �bersicht schaffen, in welchem Verzeichnis eventuell Rechte fehlen. Desweiteren wird �berpr�ft ob Drucker vorhanden sind und ob diese f�r den ASPNET User zug�nglich sind. Ist dies nicht der Fall, so tritt bei Ihrer Webapplikation ein Fehler auf, da List & Label einen korrekt installierten Drucker ben�tigt. Damit Sie nun Ihre Webanwendung mit dem Tool pr�fen lassen k�nnen, sind noch folgende Schritte notwendig:

1. Bitte kopieren Sie zun�chst aus dem Verzeichnis "\llwebcheck" die Dateien "ll19webcheck.exe" und "llwebcheck.aspx" in Ihr Web-Hauptverzeichnis.

2. Anschliessend kopieren Sie bitte die Datei "llwebcheck.dll" in das Unterverzeichnis "\bin". Sollte dies nicht vorhanden sein, so erstellen Sie dieses.

3. Starten Sie nun die Anwendung "ll19webcheck.exe" und geben Sie ggf. die URL Ihrer Webanwendung an.

Stellt sich beim Ausf�hren der Anwendung heraus das der ASPNET User keine Rechte auf ein bestimmtes Verzeichnis hat, so m�ssen Sie diesem explizit diese Rechte zuweisen. In dem Fall, dass keine Drucker aufgelistet werden oder diese lediglich Rot dargestellt sind, m�ssen Sie ebenfalls die Rechte f�r die entsprechenden Drucker vergeben. Die Microsoft Knowledgebase h�lt unter der Nummer Q184291 einen englischsprachigen Artikel bereit, wie zur Einrichtung von Druckern f�r den SYSTEM beziehungsweise ASPNET Account vorzugehen ist. Sie erreichen den Artikel �ber den angegebenen Link. Nach erfolgreicher �bernahme der Konfiguration sollte der Druck innerhalb des Services m�glich sein.

http://support.microsoft.com/support/kb/articles/Q184/2/91.ASP

Hinweis: Sollten Sie die Anwendung ll19webcheck.exe von einem separaten Rechner aus starten beachten Sie bitte, dass die Rechte / Informationen vom Webserver abgefragt werden und _nicht_ vom aufrufenden Rechner!
package guessMyMusicApp.raspberryPi.shell;

import java.io.*;
import java.util.List;

public class shellController {

    public static String executeCommand(String workingDir, List<String> commands, boolean waitForResponse) {
        String response = "";

        try {
            ProcessBuilder pb = new ProcessBuilder(commands);
            pb.directory(new File(workingDir));
            Process process = pb.start();

            if (waitForResponse) {
                //get response
                InputStream shellIn = process.getInputStream();
                int shellExitStatus = process.waitFor();
                System.out.println("Exit status" + shellExitStatus);
                response = convertStreamToStr(shellIn);
            } else {
                return "";
            }

        } catch (IOException e) {
            System.out.println("Error occured while executing Linux command. Error Description: "
                    + e.getMessage());
        } catch (InterruptedException e) {
            System.out.println("Error occured while executing Linux command. Error Description: "
                    + e.getMessage());
        }

        return response;
    }

    /*
     * To convert the InputStream to String we use the Reader.read(char[]
     * buffer) method. We iterate until the Reader return -1 which means
     * there's no more data to read. We use the StringWriter class to
     * produce the string.
     */

    static String convertStreamToStr(InputStream is) throws IOException {

        if (is != null) {
            Writer writer = new StringWriter();

            char[] buffer = new char[1024];
            try {
                Reader reader = new BufferedReader(new InputStreamReader(is,
                        "UTF-8"));
                int n;
                while ((n = reader.read(buffer)) != -1) {
                    writer.write(buffer, 0, n);
                }
            } finally {
                is.close();
            }
            return writer.toString();
        } else {
            return "";
        }
    }

}


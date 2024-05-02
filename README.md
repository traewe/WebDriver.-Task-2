## Task Description
When performing a task, you must use the capabilities of Selenium WebDriver, a unit test framework (for example NUnit) and the Page Object concept.

Automate the following script:

1. Open [https://pastebin.com/](https://pastebin.com/) or a similar service in any browser.
2. Create 'New Paste' with the following attributes:
   - Code:
     ```bash
     git config --global user.name  "New Sheriff in Town"
     git reset $(git commit-tree HEAD^{tree} -m "Legacy code")
     git push origin master --force
     ```
   - Syntax Highlighting: "Bash"
   - Paste Expiration: "10 Minutes"
   - Paste Name / Title: "how to gain dominance among developers"
3. Save 'paste' and check the following:
   - Browser page title matches 'Paste Name / Title'
   - Syntax is suspended for bash
   - Check that the code matches the one from paragraph 2

<table border="1">
  <tr>
    <th>Criteria for successful completion</th>
    <th>Rate</th>
  </tr>
  <tr>
    <td>The project is set up. All required dependencies are set with the build tool (e.g., pom.xml for maven).</td>
    <td>20%</td>
  </tr>
  <tr>
    <td>Script is written based on the provided scenario.</td>
    <td>40%</td>
  </tr>
  <tr>
    <td>Code is written in a well-structured manner. All best practices are followed. Code is easy to read and maintain.</td>
    <td>10%</td>
  </tr>
  <tr>
    <td>Script is stabilized. No intermittent failures during the run. It passes during 5 subsequent runs.</td>
    <td>30%</td>
  </tr>
</table>

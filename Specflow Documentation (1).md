
<h1>SPECFLOW DOCUMENTATION</h1>

<h2>Overview</h2>
<text>Specflow is a testing framework which supports Behavior Driven Development (BDD). It lets us define application behavior in plain meaningful English text using a simple grammar defined by a language called Gherkin. </text>

<h2>Principles of Specflow</h2>
<text>  - The title should describe an activity
- The narrative should include a role, a feature, and a benefit 
- The scenario title should clearly explain the scenario
- The scenario should be described in terms of given, events, and outcomes
</text>

<h2>Principles of BDD</h2>
<text> - Defines a test set for the unit first
 - Make the test fail
 - Then implement the unit
 - Finally verify that the implementation of the unit makes the test succeed
</text>


----------

<h2>Specflow Installation</h2>
<text>If you never used Specflow with your Visual Studio you have to add the Specflow extension to your application. 

 1. Go to the tools drop down and choose  extensions and updates
 2. Search for Specflow and install 

![Specflow extension](https://lh3.googleusercontent.com/y8LGf3rkd3Qvms8ONYaRZgL1yNA6nr7bcVcpOajf84k0srDSu2CH6sJ8iEvIalc_x7kNhjU=s0 "Specflow extension")
You only have to install this extension once and it will be including in any future project.

Once you have the extension installed you can add unit testing to your project.

 1. Within the solution explorer, right click on solution and go to add.
 2. Add a new unit test project to the solution.

Once the unit test project is added successfully you have to add some packages to the project.

 1. Click on tools and go to the NuGet package manager.
 2. Search for and install 
 -NUnit by Charlie Poole
 -Selenium.WebDriver by Selenium Committers
 -Selenuim.Support  by Selenium Committers
 -SpecRun.Specflow by TechTalk (Installing SpecRun.Specflow should automatically install Specflow and SpecRun.Runner due to dependencies)
 **There is a known bug within the latest stable release of SpecRun.Specflow to get around this download the latest prerelease 1.6.0-rc003**

 ![enter image description here](https://lh3.googleusercontent.com/QUm7e-ylAz1MUHX6ekb98a5oL2X390YnvSTmubLlI3UtIo3avttlwNu38WL_t4PfufRAv1U=s0 "specflow packages.PNG")
</text>


----------

<h2>Adding A Feature File</h2>

<text>Once Specflow is installed we have to add a feature file to our project.

 1. From the solution explorer right click on the unit test project and click add.
 2. Click on new item and search for a feature file.
 ![enter image description here](https://lh3.googleusercontent.com/-kUWnP8vW7wg/WOU0zzqs5DI/AAAAAAAAAA4/5GnJIJLtsEsio_9-Ia_E7TloSw8ksFeBQCLcB/s0/specflow6.PNG "specflow feature.PNG") 
 3. Add the feature file to the project.
 4. Once added Specflow will generate a default scenario of adding two numbers together.

Once you update you scenario we have to generate the step definition.

 1. Right click within the feature file.
 2. Go to generate step definition.
 ![enter image description here](https://lh3.googleusercontent.com/oPOi3XXzgO5hnJ83wqE_Txv9L9pcgr72k4__f-WRKVF9UHjTyiv1xFt27j3CoD3ZUh999T4=s0 "specflow steps generation.PNG")  
 
 3. Save the generated file within the project folder.

</text>


----------

<h2>Specflow Feature Steps</h2>

<text>Now we should have a new skeleton class for each of the steps in our scenario.
![enter image description here](https://lh3.googleusercontent.com/-OGs-YnhuqhA/WOU3TOfQmwI/AAAAAAAAABA/T5DMgkiTS2oa1wuomuPLL_q62HB3XCu3wCLcB/s0/InitialSkeleton.png "InitialSkeleton.png")

Because Specflow uses regular expressions it makes it easy to pass variables from the story to the function. 

</text>


----------
<h2></h2>

<text>In order to access a web browser we have to include a webdriver in our project. We already downloaded the packages for the Selenium webdriver during the installation process. But we also have to add the browser driver (in this case I'm using Chrome) to the project. 

 1. Open the NuGet package manager console.
 2. Run PM> Install-Package WebDriver.ChromeDriver.win32

![enter image description here](https://lh3.googleusercontent.com/-TPeChcFuG2I/WOU5EAA1-8I/AAAAAAAAABM/PVCAxtWygWUaiE6qXT5hrM72Hjc6O0O2QCLcB/s0/specflow4.PNG "specflow webdriver.PNG")

Once we successfully add the webdriver to the project we can initialize it. I done so using the [BeforeScenario] Attribute and disposed of it using [AfterScenario].

When using the Selenium driver the easiest way to find elements on the page is by using the xpath. This path can be copied from the inspection console.
(From Chrome)
 1. On the webpage find the element you want to get the xpath for.
 2. Right click on the element and click inspect.
 3. The element you clicked on should now be highlighted in blue. Right click on this and go to copy xpath.
 ![enter image description here](https://lh3.googleusercontent.com/-JS0COx1pfr4/WOVAgKSY-lI/AAAAAAAAABw/ricf35IQWE81TYhz30teR_5YfSSlyrFZgCLcB/s0/specflow.PNG "specflow xpath.PNG")
 
 4. Once you copy the xpath you can now tell the webdriver to find that element by the path.
 ![enter image description here](https://lh3.googleusercontent.com/-w2l21AbDi9g/WOVBHxWbKEI/AAAAAAAAAB4/M8X7qgK_4xoAtEK8AkIFhtMEkAKFTzaxwCLcB/s0/specflow8.PNG "specflow code xpath.PNG")

</text>


----------

<h2>Caveat</h2>

<text>

 - When using the webdriver if any item on the page is covered by other elements the program won't be able to find that element. 
![enter image description here](https://lh3.googleusercontent.com/-iO9pxc0pQmQ/WOU9D7bqarI/AAAAAAAAABc/5uV5L5CD6ZE4nWaAnW8ZDmC3sV8rrSBcACLcB/s0/specflow7.PNG "specflow element hidden.PNG")
All elements behind this banner are not accessible until the banner is moved or closed.

 - There is a known bug within the latest stable release of SpecRun.Specflow to get around this download the latest prerelease 1.6.0-rc003

</text>


----------

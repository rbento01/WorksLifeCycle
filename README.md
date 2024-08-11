# IPT Works LifeCycle (ipt-wlc)
_IPT's academic projects submission system._

Web application to support the entire life cycle of academic works (focused on MSc theses) carried out at the Polytechnic Institute of Tomar (IPT).

This includes:
- [ ] Project proposal by supervisors
- [ ] Enrolment of students to project/courses by staff
- [ ] Applications (to proposals) by students
- [ ] Project-application approvals based on rankings (grades, remaining credits)
- [ ] Thesis/report submission by students
- [ ] Assignment of juri members and public defence details by course director(?)
- [ ] Access to project report/thesis by juri members
- [ ] Grade assignment (+minute/ata?) by juri, confirmed by staff?
- [ ] Define degree completion by academic staff
- [ ] Export data to RCAAP and RENATES by documentation/library staff

## Installation

TO DO
The idea is to use docker containers and docker-compose to launch the solution, thus the instructions will be more or less: 
```bash
# clone the project
git clone <url>

# run everything, probably seed DB with courses and default users
docker-compose up -d
```

## Contributing
To contribute clone the project and start developing on a different branch. Pull requests are welcome.

Please make sure to update tests as appropriate.

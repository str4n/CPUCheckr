create table Processors
(
    Id           char(36) charset ascii not null
        primary key,
    Manufacturer longtext               not null,
    Model        longtext               not null,
    Cores        int                    not null,
    ClockRate    longtext               not null,
    Socket       longtext               not null,
    Price        double                 null
);


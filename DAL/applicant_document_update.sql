create sequence lookup_id_seq;
alter table mp_lookup alter id set default nextval('lookup_id_seq');

ALTER TABLE public.mp_applicant
ADD COLUMN  expertise text COLLATE pg_catalog."default";
ALTER TABLE public.mp_applicant
ADD COLUMN  about text COLLATE pg_catalog."default";

create sequence applicant_document_seq;
alter table mp_applicant_document alter id set default nextval('applicant_document_seq');

INSERT INTO public.mp_lookup (id,category,value,deleted) VALUES (3,'application_status','pending',0)
,(4,'application_status','awaiting_interview',0)
,(5,'application_status','hired',0);